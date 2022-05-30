using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Annonser.Pages
{
    public class AnnonsModel : PageModel
    {
        public AdHandler AnnonsHanterare { get; }


        [BindProperty]
        public TorgetAd TorgetAd { get; set; }

        public AnnonsModel(AdHandler annonsHanterare)
        {
            AnnonsHanterare = annonsHanterare;
        }

        public async Task OnGetAsync()
        {
            TorgetAd = await AnnonsHanterare.Get(1);
        }
    }
}
