namespace CurrencyDataLibrary.DataSaving
{
    public interface IPathHandler
    {
        string GetFullPath(string filePath, string fileExtension);
        void EnsureDirectoryExists(string path);
    }
}
