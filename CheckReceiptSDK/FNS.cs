using CheckReceiptSDK.Resources;
using CheckReceiptSDK.Results;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("CheckReceiptSDK.Tests")]
namespace CheckReceiptSDK
{
    /// <summary>
    /// Class for communication with Russian federal tax service
    /// </summary>
    public sealed class FNS
    {
        private readonly HttpClient _client;

        private static FNS _instance;
        /// <summary>
        /// Instance of class for communication with federal tax service
        /// </summary>
        public static FNS Instance => _instance ?? (_instance = new FNS());

        private FNS()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// Constructor for unit testing
        /// </summary>
        /// <param name="client"></param>
        internal FNS(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// New user registration. User is needed for getting receipt's detailed information.
        /// </summary>
        /// <param name="email">User email address</param>
        /// <param name="name">User name</param>
        /// <param name="phone">User phone number in format +79991234567</param>
        /// <returns></returns>
        public async Task<Result> RegisterAsync(string email, string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Unacceptable parameter value", nameof(email));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Unacceptable parameter value", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Unacceptable parameter value", nameof(phone));
            }

            var requestContent = new StringContent(JsonConvert.SerializeObject(new { phone, email, name }));
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(Urls.Registration, requestContent);

            return await GetResultAsync(response);
        }

        /// <summary>
        /// Sign in. Actually it is not needed.
        /// </summary>
        /// <param name="phone">User phone number in format +79991234567</param>
        /// <param name="password">User password that user got via SMS during registration or password restore.</param>
        /// <returns>Email and user name specified on registration</returns>
        public async Task<Result> LoginAsync(string phone, string password)
        {
            AddAuthorizationTokenToHeaders(phone, password);

            var response = await _client.GetAsync(Urls.Login);

            return await GetResultAsync(response);
        }

        /// <summary>
        /// Password restore. Restored password will be sent to user in SMS.
        /// </summary>
        /// <param name="phone">User phone number in format +79991234567</param>
        /// <returns></returns>
        public async Task<Result> RestorePasswordAsync(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Unacceptable parameter value", nameof(phone));
            }
            var requestContent = new StringContent(JsonConvert.SerializeObject(new { phone }));
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(Urls.Restore, requestContent);

            return await GetResultAsync(response);
        }

        private async Task<Result> GetResultAsync(HttpResponseMessage response) => new Result
        {
            IsSuccess = response.IsSuccessStatusCode,
            Message = await response.Content.ReadAsStringAsync(),
            StatusCode = response.StatusCode
        };

        /// <summary>
        /// Check if federal tax service got the receipt.
        /// </summary>
        /// <param name="fiscalNumber">
        /// Fiscal number, so called as FN. Number consists of 16 digits.
        /// Фискальный номер, также известный как ФН. Номер состоит из 16 цифр.
        /// </param>
        /// <param name="fiscalDocument">
        /// Fiscal document number, so called FD. Number consists of 10 digits max.
        /// Номер фискального документа, также известный как ФД. Состоит максимум из 10 цифр.
        /// </param>
        /// <param name="fiscalSign">
        /// Fiscal sign (attribute), so called ФП, ФПД. Consists of 10 digits max.
        /// Фискальный признак документа, также известный как ФП, ФПД. Состоит максимум из 10 цифр.
        /// </param>
        /// <param name="date">Date specified in receipt. Seconds are not required.</param>
        /// <param name="sum">Amount specified in receipt. In rubles. Includes kopecks.</param>
        /// <returns></returns>
        public async Task<CheckResult> CheckAsync(string fiscalNumber, string fiscalDocument, string fiscalSign, DateTime date, decimal sum)
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
        /// Get detailed receipt's information.
        /// Before calling this method <see cref="CheckAsync(string, string, string, DateTime, decimal)"/>
        /// should be called.
        /// </summary>
        /// <param name="fiscalNumber">
        /// Fiscal number, so called as FN. Number consists of 16 digits.
        /// Фискальный номер, также известный как ФН. Номер состоит из 16 цифр.
        /// </param>
        /// <param name="fiscalDocument">
        /// Fiscal document number, so called FD. Number consists of 10 digits max.
        /// Номер фискального документа, также известный как ФД. Состоит максимум из 10 цифр.
        /// </param>
        /// <param name="fiscalSign">
        /// Fiscal sign (attribute), so called ФП, ФПД. Consists of 10 digits max.
        /// Фискальный признак документа, также известный как ФП, ФПД. Состоит максимум из 10 цифр.
        /// </param>
        /// <param name="phone">User phone number in format +79991234567</param>
        /// <param name="password">User password that user got via SMS during registration or password restore.</param>
        /// <returns>Receipt's information</returns>
        public async Task<ReceiptResult> ReceiveAsync(string fiscalNumber, string fiscalDocument, string fiscalSign, string phone, string password)
        {
            AddAuthorizationTokenToHeaders(phone, password);
            AddRequiredHeaders();

            var response = await _client.GetAsync(Urls.GetReceiveUrl(fiscalNumber, fiscalDocument, fiscalSign));
            var content = await response.Content.ReadAsStringAsync();
            var result = new ReceiptResult
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Content = content
            };

            try
            {
                result.Document = JsonConvert.DeserializeObject<RootObject>(content).Document;
            }
            catch
            {
                result.Message = content;
            }
            return result;
        }

        /// <summary>
        /// Some methods require specific headers. This method adds them.
        /// </summary>
        private void AddRequiredHeaders()
        {
            if (!_client.DefaultRequestHeaders.Contains("Device-Id"))
            {
                _client.DefaultRequestHeaders.Add("Device-Id", string.Empty);
                _client.DefaultRequestHeaders.Add("Device-OS", string.Empty);
            }
        }

        /// <summary>
        /// Some methods require authorization. This method adds that authorization.
        /// </summary>
        /// <param name="phone">User phone number for authorization</param>
        /// <param name="password">User password for authorization</param>
        private void AddAuthorizationTokenToHeaders(string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Unacceptable parameter value", nameof(phone));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Unacceptable parameter value", nameof(password));
            }
            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
            {
                var credentialBuffer = new UTF8Encoding().GetBytes($"{phone}:{password}");
                _client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(credentialBuffer)}");
            }
        }
    }
}
