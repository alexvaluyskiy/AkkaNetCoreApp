using System.Net.Http;

namespace AkkaNetCoreApp.Models
{
    public class DownloadHttpResponse
    {
        public DownloadHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public HttpResponseMessage HttpResponseMessage { get; }
    }
}
