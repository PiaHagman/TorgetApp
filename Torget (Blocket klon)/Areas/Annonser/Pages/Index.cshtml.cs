using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.ModelsNotInDb;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Annonser.Pages;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly AdHandler _adHandler;
    [BindProperty(SupportsGet = true)] public SearchQuery? SearchQuery { get; set; }
    public List<TorgetAd> TorgetAds { get; set; }

    public IndexModel(AdHandler adHandler)
    {
        _adHandler = adHandler;
    }

    public async Task<IActionResult> OnGet()
    {
        TorgetAds = await _adHandler.GetList(SearchQuery);

        return Page();
    }
}