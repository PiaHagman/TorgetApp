using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{
    [Authorize]
    public class MinaAnnonserModel : PageModel
    {
        private readonly UserManager<TorgetUser> _userManager;
        private readonly AdHandler _adHandler;


        [BindProperty] public List<TorgetAd> MinaAnnonser { get; set; }

        public MinaAnnonserModel(UserManager<TorgetUser> userManager, AdHandler adHandler)
        {
            _userManager = userManager;
            _adHandler = adHandler;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            MinaAnnonser = await _adHandler.GetUserAds(userId);

            return Page();
        }

        public IActionResult OnPost(int torgetAdId)
        {
            return RedirectToPage("/ÄndraAnnons", new {adId = torgetAdId});
        }
    }
}