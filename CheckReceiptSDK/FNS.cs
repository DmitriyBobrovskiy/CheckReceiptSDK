using CheckReceiptSDK.Resources;
using CheckReceiptSDK.Results;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CheckReceiptSDK
{
    /// <summary>
    /// Класс для взаимодействия с ФНС
    /// </summary>
    public static class FNS
    {
        private static HttpClient _client = new HttpClient();

        /// <summary>
        /// Регистрация нового пользователя. Необходима для получения детальной информации по чекам.
        /// </summary>
        /// <param name="email">Электронный адрес пользователя</param>
        /// <param name="name">Имя пользователя</param>
        /// <param name="phone">Номер телефона пользователя в формате +79991234567</param>
        /// <returns></returns>
        public static async Task<Result> RegistrationAsync(string email, string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(email));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(phone));
            }

            var requestContent = new StringContent(JsonConvert.SerializeObject(new { phone, email, name }));
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(Urls.Registration, requestContent);

            return await GetResultAsync(response);
        }

        /// <summary>
        /// Аутентификация пользователя. Необходимости в ней нет, но раз ФНС предоставляет, как не воспользоваться.
        /// </summary>
        /// <param name="phone">Номер телефона пользователя в формате +79991234567</param>
        /// <param name="password">Пароль пользователя, который он получал из СМС при регистрации или восстановлении пароля</param>
        /// <returns>Возвращает адрес электронной почты и имя указанные при регистрации</returns>
        public static async Task<Result> LoginAsync(string phone, string password)
        {
            AddAuthorizationTokenToHeaders(phone, password);

            var response = await _client.GetAsync(Urls.Login);

            return await GetResultAsync(response);
        }

        /// <summary>
        /// Восстановление пароля. Восстановленный пароль придет в СМС.
        /// </summary>
        /// <param name="phone">Номер телефона в формате +79991234567</param>
        /// <returns></returns>
        public static async Task<Result> RestorePasswordAsync(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(phone));
            }
            var requestContent = new StringContent(JsonConvert.SerializeObject(new { phone }));
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(Urls.Restore, requestContent);

            return await GetResultAsync(response);
        }

        private static async Task<Result> GetResultAsync(HttpResponseMessage response) => new Result
        {
            IsSuccess = response.IsSuccessStatusCode,
            Message = await response.Content.ReadAsStringAsync(),
            StatusCode = response.StatusCode
        };

        /// <summary>
        /// Проверить поступил ли чек в ФНС
        /// </summary>
        /// <param name="fiscalNumber">Фискальный номер, также известный как ФН. Номер состоит из 16 цифр.</param>
        /// <param name="fiscalDocument">Номер фискального документа, также известный как ФД. Состоит максимум из 10 цифр.</param>
        /// <param name="fiscalSign">Фискальный признак документа, также известный как ФП, ФПД. Состоит максимум из 10 цифр.</param>
        /// <param name="date">Дата, указанная в чеке. Секунды не обязательные.</param>
        /// <param name="sum">Сумма, указанная в чеке. Включая копейки.</param>
        /// <returns></returns>
        public static async Task<CheckResult> CheckAsync(string fiscalNumber, string fiscalDocument, string fiscalSign, DateTime date, decimal sum)
        {
            var response = await _client.GetAsync(Urls.GetCheckUrl(fiscalNumber, fiscalDocument, fiscalSign, date, sum));
            var result = new CheckResult
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                ReceiptExists = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode
            };

            return result;
        }

        /// <summary>
        /// Получить детальную информацию по чеку. Если перед этим не проверялось поступил ли он в ФНС, 
        /// то при первом обращении данный метод вернет лишь 202 Accepted(Это не правда. Он возвращает ошибку,
        /// и чтобы всё хорошо работало мне приходится сначала через телефон активировать) и никакой информации по чеку.
        /// При повторном будет вся необходимая информация.
        /// </summary>
        /// <param name="fiscalNumber">Фискальный номер, также известный как ФН. Номер состоит из 16 цифр.</param>
        /// <param name="fiscalDocument">Номер фискального документа, также известный как ФД. Состоит максимум из 10 цифр.</param>
        /// <param name="fiscalSign">Фискальный признак документа, также известный как ФП, ФПД. Состоит максимум из 10 цифр.</param>
        /// <param name="phone">Номер телефона в формате +79991234567, использованный при регистрации</param>
        /// <param name="password">Пароль пользователя, полученный в СМС</param>
        /// <returns>Возвращает информацию по чеку</returns>
        public static async Task<ReceiptResult> ReceiveAsync(string fiscalNumber, string fiscalDocument, string fiscalSign, string phone, string password)
        {
            AddAuthorizationTokenToHeaders(phone, password);
            AddRequiredHeaders();

            var response = await _client.GetAsync(Urls.GetReceiveUrl(fiscalNumber, fiscalDocument, fiscalSign));
            var result = new ReceiptResult
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
            };

            try
            {
                result.Document = JsonConvert.DeserializeObject<RootObject>(await response.Content.ReadAsStringAsync()).Document;
            }
            catch
            {
                result.Message = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        /// <summary>
        /// Некоторые методы требуют специальных заголовков. Данный метод добавляет их.
        /// </summary>
        private static void AddRequiredHeaders()
        {
            if (!_client.DefaultRequestHeaders.Contains("Device-Id"))
            {
                _client.DefaultRequestHeaders.Add("Device-Id", string.Empty);
                _client.DefaultRequestHeaders.Add("Device-OS", string.Empty);
            }
        }

        /// <summary>
        /// Некоторые методы требуют авторизации. Данный метод добавляет эту авторизацию.
        /// </summary>
        /// <param name="phone">Номер телефона для авторизации</param>
        /// <param name="password">Пароль пользователя для авторизации</param>
        private static void AddAuthorizationTokenToHeaders(string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(phone));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(password));
            }
            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
            {
                var credentialBuffer = new UTF8Encoding().GetBytes($"{phone}:{password}");
                _client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(credentialBuffer)}");
            }
        }
    }
}
