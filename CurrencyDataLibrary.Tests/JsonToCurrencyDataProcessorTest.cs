using CurrencyDataLibrary.ApiCommunication.Impl;
using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.Tests
{
    [TestClass]
    public class JsonToCurrencyDataProcessorTest
    {
        private readonly string validJson =
        @"[
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

        private List<CurrencyData> expectedCurrencyData =
        [
            new() {
                Id = 36,
                CurrencyCode = "AUD",
                FullName = "Австралійський долар",
                Rate = 25.8816m,
                Timestamp = new DateTime(2024, 04, 29)
            },
            new() {
                Id = 156,
                CurrencyCode = "CNY",
                FullName = "Юань Женьміньбі",
                Rate = 5.4649m,
                Timestamp = new DateTime(2024, 04, 29)
            }
        ];

        [TestMethod]
        public void FetchCurrencyDataReturnsCurrencyData()
        {
            var processor = new JsonToCurrencyDataProcessor();
            List<CurrencyData> result = processor.ProcessJson(validJson);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCurrencyData.Count, result.Count);
            // Check the content of first currency data
            for (int i = 0; i < expectedCurrencyData.Count; i++)
            {
                Assert.AreEqual(expectedCurrencyData[i].Id, result[i].Id);
                Assert.AreEqual(expectedCurrencyData[i].CurrencyCode, result[i].CurrencyCode);
                Assert.AreEqual(expectedCurrencyData[i].FullName, result[i].FullName);
                Assert.AreEqual(expectedCurrencyData[i].Rate, result[i].Rate);
                Assert.AreEqual(expectedCurrencyData[i].Timestamp, result[i].Timestamp);
            }
        }
    }
}