using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;


namespace Torget__Blocket_klon_.Areas.Annonser.Pages
{
    public class AnnonsModel : PageModel
    {
        [BindProperty]
        public Annons Annons { get; set; }

        public AnnonsModel()
        {
            
        }

        public void OnGet()
        {

        }
    }
}
