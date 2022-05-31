using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Annonser.Pages
{
    public class AnnonsModel : PageModel
    {
        public AnnonsHanterare AnnonsHanterare { get; }


        [BindProperty]
        public TorgetAd TorgetAd { get; set; }
        public int ImagesCount => TorgetAd.AdImages.Count;
        public int TagsCount => TorgetAd.Tags.Count;

        public AnnonsModel(AnnonsHanterare annonsHanterare)
        {
            AnnonsHanterare = annonsHanterare;
        }

        public async Task OnGetAsync()
        {
            TorgetAd = await AnnonsHanterare.Get(1);

           
        }
    }
}
