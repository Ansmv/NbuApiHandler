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

    [TestClass]
    public class FileNameGeneratorTests
    {
        [TestMethod]
        public void GenerateFileName_ReturnsCorrectFormat()
        {
            var mockClock = new Mock<ISystemClock>();
            var datetime = new DateTime(2024, 5, 2, 12, 0, 0);
            mockClock.Setup(clock => clock.UtcNow).Returns(datetime);

            var fileNameGenerator = new FileNameGenerator(mockClock.Object);

            var resultCsv = fileNameGenerator.GenerateFileName(".csv");
            var resultJson = fileNameGenerator.GenerateFileName(".json");
            var resultXml = fileNameGenerator.GenerateFileName(".xml");
            Assert.AreEqual("2024-05-02_12-00-00_currency_data.csv", resultCsv);
            Assert.AreEqual("2024-05-02_12-00-00_currency_data.json", resultJson);
            Assert.AreEqual("2024-05-02_12-00-00_currency_data.xml", resultXml);
        }
        [TestMethod]
        public void GenerateFileName_ThrowsExceptionForUnknownFormat()
        {
            var mockClock = new Mock<ISystemClock>();
            var dateTime = new DateTime(2024, 5, 2, 12, 0, 0);
            mockClock.Setup(c => c.UtcNow).Returns(dateTime);

            var fileNameGenerator = new FileNameGenerator(mockClock.Object);
            Assert.ThrowsException<NotImplementedException>(() => fileNameGenerator.GenerateFileName(".unknown"));
        }
    }
}
