namespace CurrencyDataLibrary.DataSaving.Abstr
{
    public interface IFileNameGenerator
    {
        string GenerateFileName(string fileExtension);
    }
}
