using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CheckReceiptSDK.Tests
{
    internal class HttpMessageHandlerMock : HttpMessageHandler
    {
        private HttpStatusCode _statusCode;
        private string _content;

        internal HttpMessageHandlerMock(HttpStatusCode statusCode, string content)
        {
            _statusCode = statusCode;
            _content = content;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = _statusCode,
                Content = new StringContent(_content)
            });
        }

        internal static HttpClient GetHttpClient(HttpStatusCode statusCode, string content)
        {
            return new HttpClient(new HttpMessageHandlerMock(statusCode, content));
        }
    }
}
