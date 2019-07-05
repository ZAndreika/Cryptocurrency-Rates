using CryptocurrencyRateWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace CryptocurrencyRateWebApp.Services {
    public class CoinMarketCapApiWorker {
        private static string API_KEY = "53f581ca-11e0-4d2d-8a1c-8173b9407662";

        /// <summary>
        /// Get information about cryptocurrencies from CoinMarketCap website by CoinMarketCap API
        /// </summary>
        /// <returns>string in json format with information about cryptocurrencies</returns>
        public string GetLatestListing() {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "500";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }

        /// <summary>
        /// Get information about currencies which contains logos links
        /// </summary>
        /// <param name="symbols">string with symbols for which need to get info</param>
        /// <returns>JToken which contains logos links</returns>
        public JToken GetLogos(string symbols) {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/info");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbols;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");

            JObject json = JObject.Parse(client.DownloadString(URL.ToString()));
            return json["data"];
        }

        /// <summary>
        /// Parse API answers and generate list of cryptocurrencies with needed params
        /// </summary>
        /// <returns>list of cryptocurrencies</returns>
        public IEnumerable<Cryptocurrency> GetCrytpocurrencyRateList() {
            string listing;
            try {
                listing = GetLatestListing();
            }
            catch (WebException) {
                throw;
            }

            JObject json;
            json = JObject.Parse(listing);

            // Deserialize parameters that are possible
            IEnumerable<Cryptocurrency> Cryptocurrencies;
            Cryptocurrencies = JsonConvert.DeserializeObject<List<Cryptocurrency>>(json["data"].ToString());

            // list of symbols of every currency to form request for get logos
            List<string> symbols = new List<string>();

            // Add missing parameters to every currency model
            int i = 0;
            foreach (var currency in Cryptocurrencies) {
                var price = json["data"][i]["quote"]["USD"]["price"];
                currency.Price = price.Type != JTokenType.Null ? (double)price : 0;

                var percent_change_1h = json["data"][i]["quote"]["USD"]["percent_change_1h"];
                currency.Percent_change_1h = percent_change_1h.Type != JTokenType.Null ? (double)percent_change_1h : 0;

                var percent_change_24h = json["data"][i]["quote"]["USD"]["percent_change_24h"];
                currency.Percent_change_24h = percent_change_24h.Type != JTokenType.Null ? (double)percent_change_24h : 0;

                var market_cap = json["data"][i]["quote"]["USD"]["market_cap"];
                currency.Market_cap = market_cap.Type != JTokenType.Null ? (double)market_cap : 0;

                i++;
                symbols.Add(currency.Symbol.ToString()); // generate list of symbols for next logo request 
            }

            JToken logos;
            try {
                logos = GetLogos(string.Join(",", symbols));
            }
            catch (WebException) {
                throw;
            }

            // add logo link for every currency
            foreach (var currency in Cryptocurrencies) {
                currency.Logo = (string)logos[currency.Symbol.ToString()]["logo"];
            }

            return Cryptocurrencies;
        }
    }
}