using CryptocurrencyRateWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace CryptocurrencyRateWebApp.Services
{
    public class CoinMarketCapApiService : IApiWorker
    {
        private static string api_key = "53f581ca-11e0-4d2d-8a1c-8173b9407662";

        /// <summary>
        /// Get information about cryptocurrencies from CoinMarketCap website by CoinMarketCap API
        /// </summary>
        /// <returns>JToken with cryptocurrencies list</returns>
        public JToken GetLatestListing()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "500";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", api_key);
            client.Headers.Add("Accepts", "application/json");

            JObject json;
            json = JObject.Parse(client.DownloadString(URL.ToString()));
            return json["data"];
        }

        /// <summary>
        /// Get information about currencies which contains logos links
        /// </summary>
        /// <param name="symbols">string with symbols for which need to get info</param>
        /// <returns>JToken which contains logos links</returns>
        private JToken GetLogos(string symbols)
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/info");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbols;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", api_key);
            client.Headers.Add("Accepts", "application/json");

            JObject json = JObject.Parse(client.DownloadString(URL.ToString()));
            return json["data"];
        }

        /// <summary>
        /// Parse API answers and generate list of cryptocurrencies with needed params
        /// </summary>
        /// <returns>list of cryptocurrencies</returns>
        public IEnumerable<Cryptocurrency> GetCrytpocurrenciesList()
        {
            JToken listing;
            try
            {
                listing = GetLatestListing();
            }
            catch (WebException)
            {
                throw;
            }

            // Deserialize parameters that are possible
            IEnumerable<Cryptocurrency> cryptocurrencies;
            cryptocurrencies = JsonConvert.DeserializeObject<List<Cryptocurrency>>(listing.ToString());

            // list of symbols of every currency to form request for get logos
            List<string> symbols = new List<string>();

            // Add missing parameters to every currency model
            int i = 0;
            foreach (var currency in cryptocurrencies)
            {
                JToken quote = listing[i]["quote"]["USD"];

                var price = quote["price"];
                currency.Price = price.Type != JTokenType.Null ? (double)price : 0;

                var percent_change_1h = quote["percent_change_1h"];
                currency.Percent_change_1h = percent_change_1h.Type != JTokenType.Null ? (double)percent_change_1h : 0;

                var percent_change_24h = quote["percent_change_24h"];
                currency.Percent_change_24h = percent_change_24h.Type != JTokenType.Null ? (double)percent_change_24h : 0;

                var market_cap = quote["market_cap"];
                currency.Market_cap = market_cap.Type != JTokenType.Null ? (double)market_cap : 0;

                i++;
                symbols.Add(currency.Symbol.ToString()); // generate list of symbols for next logo request 
            }

            JToken logos;
            try
            {
                logos = GetLogos(string.Join(",", symbols));
            }
            catch (WebException)
            {
                throw;
            }

            // add logo link for every currency
            foreach (var currency in cryptocurrencies)
            {
                currency.Logo = (string)logos[currency.Symbol.ToString()]["logo"];
            }

            return cryptocurrencies;
        }
    }
}