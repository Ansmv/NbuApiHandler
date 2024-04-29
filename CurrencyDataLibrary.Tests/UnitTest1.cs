namespace CurrencyDataLibrary.Tests
{
    [TestClass]
    public class JsonFetcherTest
    {
        [TestMethod]
        public void FetchCurrencyData_ReturnsCurrencyData()
        {
            var processor = new CurrencyDataProcessor();
            string validJson = @"[
              {
                ""r030"": 36,
                ""txt"": ""Австралійський долар"",
                ""rate"": 25.8816,
                ""cc"": ""AUD"",
                ""exchangedate"": ""29.04.2024""
              },
              {
                ""r030"": 156,
                ""txt"": ""Юань Женьміньбі"",
                ""rate"": 5.4649,
                ""cc"": ""CNY"",
                ""exchangedate"": ""29.04.2024""
              }
            ]";
            List<CurrencyData> result = processor.ProcessJson(validJson);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            // Check the content of first currency data
            Assert.AreEqual(36, result[0].Id);
            Assert.AreEqual("AUD", result[0].CurrencyCode);
            Assert.AreEqual("Австралійський долар", result[0].FullName);
            Assert.AreEqual(25.8816m, result[0].Rate);
            Assert.AreEqual(new DateTime(2024, 04, 29), result[0].Timestamp);

            Assert.AreEqual(156, result[1].Id);
            Assert.AreEqual("CNY", result[1].CurrencyCode);
            Assert.AreEqual("Юань Женьміньбі", result[1].FullName);
            Assert.AreEqual(5.4649m, result[1].Rate);
            Assert.AreEqual(new DateTime(2024, 04, 29), result[1].Timestamp);
        }
    }
}