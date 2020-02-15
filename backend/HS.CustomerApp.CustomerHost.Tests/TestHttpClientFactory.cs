using System.Net.Http;

namespace HS.CustomerApp.CustomerHost.Tests
{
    internal class TestHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => new HttpClient();
    }
}