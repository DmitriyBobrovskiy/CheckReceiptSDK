using System.Net;

namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Represents response received from Russian Federal Tax Service
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Standard HTTP code
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }
        /// <summary>
        /// Content received from Federal Tax Service. May be omitted.
        /// </summary>
        public string Message { get; internal set; }
        /// <summary>
        /// Request is success
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
