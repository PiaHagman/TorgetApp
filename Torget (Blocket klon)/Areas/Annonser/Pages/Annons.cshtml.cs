using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Annonser.Pages
{
    public class AnnonsModel : PageModel
    {
        public AdHandler AdHandler { get; }


        [BindProperty]
        public TorgetAd TorgetAd { get; set; }
        public int ImagesCount => TorgetAd.AdImages.Count;
        public int TagsCount => TorgetAd.Tags.Count;

        public AnnonsModel(AdHandler adHandler)
        {
            AdHandler = adHandler;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            TorgetAd = await AdHandler.Get(1);
            return Page();
           
        }
    }
}
