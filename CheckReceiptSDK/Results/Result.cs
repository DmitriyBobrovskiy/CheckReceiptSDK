using System.Net;

namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Класс, используемый для представления ответа, полученного от ФНС
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Стандартный HTTP код
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }
        /// <summary>
        /// Ответ, полученный от ФНС. Может отсутствовать.
        /// </summary>
        public string Message { get; internal set; }
        /// <summary>
        /// Успешно ли выполнен запрос?
        /// </summary>
        public bool IsSuccess { get; internal set; }

        internal Result()
        { }

        internal Result(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
