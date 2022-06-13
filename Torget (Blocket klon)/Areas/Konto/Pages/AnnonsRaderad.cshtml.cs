using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{
    [Authorize]
    public class AnnonsRaderadModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}