using CryptocurrencyRateWebApp.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CryptocurrencyRateWebApp.Services
{
    public interface IApiWorker
    {
        JToken GetLatestListing();
        IEnumerable<Cryptocurrency> GetCrytpocurrenciesList();
    }
}
