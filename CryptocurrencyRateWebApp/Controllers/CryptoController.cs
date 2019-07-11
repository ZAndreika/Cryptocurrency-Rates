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
        public ActionResult Index(int? page, string sortField, string searchString)
        {
            ViewBag.CurrentSortField = sortField;
            ViewBag.CurrentSearchString = searchString;

            //if user click this field again sort order will change
            ViewBag.PriceSort = sortField == "Price desc" ? "Price asc" : "Price desc";
            ViewBag.NameSort = sortField == "Name desc" ? "Name asc" : "Name desc";
            ViewBag.SymbolSort = sortField == "Symbol desc" ? "Symbol asc" : "Symbol desc";
            ViewBag.Change1hSort = sortField == "Change1h desc" ? "Change1h asc" : "Change1h desc";
            ViewBag.Change24hSort = sortField == "Change1h desc" ? "Change24h asc" : "Change24h desc";
            ViewBag.MarketCapSort = sortField == "MarketCap desc" ? "MarketCap asc" : "MarketCap desc";

            CoinMarketCapApiService apiWorker = new CoinMarketCapApiService();
            IEnumerable<Cryptocurrency> cryptocurrencies;

            try
            {
                cryptocurrencies = apiWorker.GetCrytpocurrenciesList();
            }
            catch (WebException e)
            {
                return Content(e.ToString());
            }

            // if user input something into search bar
            if (!String.IsNullOrEmpty(searchString))
            {
                cryptocurrencies = cryptocurrencies.Where(x =>
                x.Name.ToUpper().Contains(searchString.ToUpper()) ||
                x.Symbol.ToUpper().Contains(searchString.ToUpper()));
            }

            // sort list of cryptocurrencies information
            switch (sortField)
            {
                case "Name desc":
                {
                    cryptocurrencies = cryptocurrencies.OrderByDescending(currency => currency.Name);
                    break;
                }
                case "Name asc":
                {
                    cryptocurrencies = cryptocurrencies.OrderBy(currency => currency.Name);
                    break;
                }
                case "Symbol desc":
                {
                    cryptocurrencies = cryptocurrencies.OrderByDescending(currency => currency.Symbol);
                    break;
                }
                case "Symbol asc":
                {
                    cryptocurrencies = cryptocurrencies.OrderBy(currency => currency.Symbol);
                    break;
                }
                case "Price desc":
                {
                    cryptocurrencies = cryptocurrencies.OrderByDescending(currency => currency.Price);
                    break;
                }
                case "Price asc":
                {
                    cryptocurrencies = cryptocurrencies.OrderBy(currency => currency.Price);
                    break;
                }
                case "Change1h desc":
                {
                    cryptocurrencies = cryptocurrencies.OrderByDescending(currency => currency.Percent_change_1h);
                    break;
                }
                case "Change1h asc":
                {
                    cryptocurrencies = cryptocurrencies.OrderBy(currency => currency.Percent_change_1h);
                    break;
                }
                case "Change24h desc":
                {
                    cryptocurrencies = cryptocurrencies.OrderByDescending(currency => currency.Percent_change_24h);
                    break;
                }
                case "Change24h asc":
                {
                    cryptocurrencies = cryptocurrencies.OrderBy(currency => currency.Percent_change_24h);
                    break;
                }
                case "MarketCap desc":
                {
                    cryptocurrencies = cryptocurrencies.OrderByDescending(currency => currency.Market_cap);
                    break;
                }
                case "MarketCap asc":
                {
                    cryptocurrencies = cryptocurrencies.OrderBy(currency => currency.Market_cap);
                    break;
                }
                default:
                {
                    break;
                }
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(cryptocurrencies.ToPagedList(pageNumber, pageSize));
        }
    }
}