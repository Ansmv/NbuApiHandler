using CurrencyDataLibrary.ApiCommunication;

namespace CurrencyDataLibrary.Tests.MockImplementations
{
    public class MockHttpClientWrapper : IHttpClientWrapper
    {
        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("Mock JSON Response")
            };
            return Task.FromResult(response);
        }
    }
}
