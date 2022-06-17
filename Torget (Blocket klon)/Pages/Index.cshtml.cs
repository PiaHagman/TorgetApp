using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.ModelsNotInDb;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private AdHandler _adhandler;

        public IndexModel(AdHandler adhandler)
        {
            _adhandler = adhandler;
        }

        public List<TorgetAd> TorgetAds { get; set; }

        public async Task OnGetAsync()
        {
            TorgetAds = await _adhandler.GetList();
        }

        // Behöver kopplas ihop med _Layout :)
        public SearchQuery GetSearchQuery()
        {

            return new SearchQuery
            {
                SearchWords = Request.Query["search"],
                Category = Request.Query["category"],
                PriceLowest = Int32.Parse(Request.Query["min-price"]),
                PriceHighest = Int32.Parse(Request.Query["max-price"])
            };
        }
    }
}
