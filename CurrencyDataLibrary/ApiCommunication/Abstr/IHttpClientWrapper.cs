namespace CurrencyDataLibrary.ApiCommunication.Abstr
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
