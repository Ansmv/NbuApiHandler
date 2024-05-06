using CurrencyDataLibrary.DataSaving;
using Moq;

namespace CurrencyDataLibrary.Tests
{
    [TestClass]
    public class PathHandlerTests
    {
        private PathHandler _pathHandler;
        private const string TestFilePath = @"C:\Temp\";
        private const string TestFileExtension = ".csv";
        private const string MockFileName = "mock.csv";

        [TestInitialize]
        public void Initialize()
        {
            SetupPathHandler();
        }

        private void SetupPathHandler()
        {
            var mockFileNameGenerator = new Mock<IFileNameGenerator>();
            mockFileNameGenerator
                .Setup(c => c.GenerateFileName(It.IsAny<string>()))
                .Returns(MockFileName);
            _pathHandler = new PathHandler(mockFileNameGenerator.Object);
        }

        [TestMethod]
        public void GetFullPath_WhenGivenAbsolutePath_ReturnsFullPathWithMockedFileName()
        {
            string fullPath = _pathHandler.GetFullPath(TestFilePath, TestFileExtension);
            Assert.AreEqual(Path.Combine(TestFilePath, MockFileName), fullPath);
        }
    }
}
