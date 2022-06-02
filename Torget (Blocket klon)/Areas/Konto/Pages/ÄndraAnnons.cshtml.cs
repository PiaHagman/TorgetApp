using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{
    public class ÄndraAnnonsModel : PageModel
    {
        private readonly AdHandler _adHandler;
        private readonly UserManager<TorgetUser> _userManager;

        public TorgetAd AdToEdit { get; set; }
       

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Rubrik")]
            public string Title { get; set; }

            [Required]
            [DataType(DataType.MultilineText)]
            [Display(Name = "Beskrivning")]
            public string Description { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "Pris")]
            public int Price { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Kategori")]
            public string Category { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Ad Image")]
            public List<IFormFile>? AdImages { get; set; }
        }

        public ÄndraAnnonsModel(AdHandler adHandler, UserManager<TorgetUser> userManager)
        {
            _adHandler = adHandler;
            _userManager = userManager;
        }

        //public async Task<IActionResult> OnGetAsync(int adId)
        //{
        //    Input = new InputModel();
        //    AdToEdit = await _adHandler.Get(adId);

        //    Input.Title = AdToEdit.Title;
        //    Input.Description = AdToEdit.Description;
        //    Input.Price = AdToEdit.Price;
        //    Input.Category = AdToEdit.Category;
        //    //Input.AdImages = AdToEdit.Category


        //}
    }
}
