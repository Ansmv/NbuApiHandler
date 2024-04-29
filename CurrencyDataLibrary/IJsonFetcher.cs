namespace CurrencyDataLibrary
{
    public interface IJsonFetcher
    {
        Task<string> FetchJsonFromApi();
    }
}
