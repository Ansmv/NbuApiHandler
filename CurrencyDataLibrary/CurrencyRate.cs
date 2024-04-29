namespace CurrencyDataLibrary
{
    public class CurrencyData
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
