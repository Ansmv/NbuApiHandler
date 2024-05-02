namespace CurrencyDataLibrary.DataSaving
{
    public interface IFileNameGenerator
    {
        string GenerateFileName(string fileExtension);
    }
}
