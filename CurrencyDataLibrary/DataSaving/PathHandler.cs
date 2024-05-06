namespace CurrencyDataLibrary.DataSaving
{
    public class PathHandler : IPathHandler
    {
        private readonly IFileNameGenerator _fileNameGenerator;

        public PathHandler(IFileNameGenerator fileNameGenerator)
        {
            _fileNameGenerator = fileNameGenerator;
        }

        public string GetFullPath(string filePath, string fileExtension)
        {
            var fullFileName = _fileNameGenerator.GenerateFileName(fileExtension);
            if (!Path.IsPathRooted(filePath))
            {
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                filePath = Path.Combine(currentDirectory, filePath);
            }
            return Path.Combine(filePath, fullFileName);
        }
    }
}
