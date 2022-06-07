using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }

    public class ChattHubb : Hub
    {
         
        public ChattHubb()
        {

        }
    }
}
