using System;

namespace CryptocurrencyRateWebApp.Models
{
    public class Cryptocurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Logo { get; set; }
        public double Price { get; set; }
        public double Percent_change_1h { get; set; }
        public double Percent_change_24h { get; set; }
        public double Market_cap { get; set; }
        public DateTime Last_updated { get; set; }
    }
}