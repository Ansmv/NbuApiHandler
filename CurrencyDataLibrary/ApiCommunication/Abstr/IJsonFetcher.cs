namespace CurrencyDataLibrary.ApiCommunication.Abstr
{
    public interface IJsonFetcher
    {
        Task<string> FetchJsonFromApi();
    }
}
