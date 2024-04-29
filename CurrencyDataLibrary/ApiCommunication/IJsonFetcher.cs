namespace CurrencyDataLibrary.ApiCommunication
{
    public interface IJsonFetcher
    {
        Task<string> FetchJsonFromApi();
    }
}
