using CurrencyDataLibrary.DataSaving.Abstr;
using Moq;

namespace CurrencyDataLibrary.Tests
{
    [TestClass]
    public class FileNameGeneratorTests
    {
        [TestMethod]
        public void GenerateFileNameReturnsCorrectFormat()
        {
            var mockClock = new Mock<ISystemClock>();
            var datetime = new DateTime(2024, 5, 2, 12, 0, 0);
            mockClock.Setup(clock => clock.UtcNow).Returns(datetime);

            var fileNameGenerator = new FileNameGenerator(mockClock.Object);

            var resultCsv = fileNameGenerator.GenerateFileName(".csv");
            var resultJson = fileNameGenerator.GenerateFileName(".json");
            var resultXml = fileNameGenerator.GenerateFileName(".xml");
            Assert.AreEqual("2024-05-02_12-00-00_csv_currency_data.csv", resultCsv);
            Assert.AreEqual("2024-05-02_12-00-00_json_currency_data.json", resultJson);
            Assert.AreEqual("2024-05-02_12-00-00_xml_currency_data.xml", resultXml);
        }

        [TestMethod]
        public void GenerateFileNameThrowsExceptionForUnknownFormat()
        {
            var mockClock = new Mock<ISystemClock>();
            var dateTime = new DateTime(2024, 5, 2, 12, 0, 0);
            mockClock.Setup(c => c.UtcNow).Returns(dateTime);

            var fileNameGenerator = new FileNameGenerator(mockClock.Object);
            Assert.ThrowsException<NotImplementedException>(() => fileNameGenerator.GenerateFileName(".unknown"));
        }
    }
}
