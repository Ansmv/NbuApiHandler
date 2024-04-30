namespace CurrencyDataLibrary.ApiCommunication
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
