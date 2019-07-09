using CryptocurrencyRateWebApp.Models;
using CryptocurrencyRateWebApp.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CryptocurrencyRateWebApp.Controllers
{
    public class CryptoController : Controller
    {
        public ActionResult Index(int? page, string sortField, string searchString) {
            ViewBag.CurrentSortField = sortField;
            ViewBag.CurrentSearchString = searchString;

            //if user click this field again sort order will change
            ViewBag.PriceSort = sortField == "Price desc" ? "Price asc" : "Price desc";
            ViewBag.NameSort = sortField == "Name desc" ? "Name asc" : "Name desc";
            ViewBag.SymbolSort = sortField == "Symbol desc" ? "Symbol asc" : "Symbol desc";
            ViewBag.Change1hSort = sortField == "Change1h desc" ? "Change1h asc" : "Change1h desc";
            ViewBag.Change24hSort = sortField == "Change1h desc" ? "Change24h asc" : "Change24h desc";
            ViewBag.MarketCapSort = sortField == "MarketCap desc" ? "MarketCap asc" : "MarketCap desc";

            CoinMarketCapApiWorker apiWorker = new CoinMarketCapApiWorker();
            IEnumerable<Cryptocurrency> Cryptocurrencies;

            try {
                Cryptocurrencies = apiWorker.GetCrytpocurrenciesList();
            }
            catch(WebException e) {
                return Content(e.ToString());
            }

            // if user input something into search bar
            if (!String.IsNullOrEmpty(searchString)) {
                Cryptocurrencies = Cryptocurrencies.Where(x =>
                x.Name.ToUpper().Contains(searchString.ToUpper()) ||
                x.Symbol.ToUpper().Contains(searchString.ToUpper()));
            }

            // sort list of cryptocurrencies information
            switch (sortField) {
                case "Name desc": {
                    Cryptocurrencies = Cryptocurrencies.OrderByDescending(currency => currency.Name);
                    break;
                }
                case "Name asc": {
                    Cryptocurrencies = Cryptocurrencies.OrderBy(currency => currency.Name);
                    break;
                }
                case "Symbol desc": {
                    Cryptocurrencies = Cryptocurrencies.OrderByDescending(currency => currency.Symbol);
                    break;
                    }
                case "Symbol asc": {
                    Cryptocurrencies = Cryptocurrencies.OrderBy(currency => currency.Symbol);
                    break;
                }
                case "Price desc": {
                    Cryptocurrencies = Cryptocurrencies.OrderByDescending(currency => currency.Price);
                    break;
                }
                case "Price asc": {
                    Cryptocurrencies = Cryptocurrencies.OrderBy(currency => currency.Price);
                    break;
                }
                case "Change1h desc": {
                    Cryptocurrencies = Cryptocurrencies.OrderByDescending(currency => currency.Percent_change_1h);
                    break;
                }
                case "Change1h asc": {
                    Cryptocurrencies = Cryptocurrencies.OrderBy(currency => currency.Percent_change_1h);
                    break;
                }
                case "Change24h desc": {
                    Cryptocurrencies = Cryptocurrencies.OrderByDescending(currency => currency.Percent_change_24h);
                    break;
                }
                case "Change24h asc": {
                    Cryptocurrencies = Cryptocurrencies.OrderBy(currency => currency.Percent_change_24h);
                    break;
                }
                case "MarketCap desc": {
                    Cryptocurrencies = Cryptocurrencies.OrderByDescending(currency => currency.Market_cap);
                    break;
                }
                case "MarketCap asc": {
                    Cryptocurrencies = Cryptocurrencies.OrderBy(currency => currency.Market_cap);
                    break;
                }
                default: {
                    break;
                }
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(Cryptocurrencies.ToPagedList(pageNumber, pageSize));
        }
    }
}