using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{
    [Authorize]
    public class ÄndraAnnonsModel : PageModel
    {
        private readonly AdHandler _adHandler;
        private readonly UserManager<TorgetUser> _userManager;

        public bool ErrorOccurred = false;

        public List<string> CategoryList { get; set; }

        public TorgetAd AdToEdit { get; set; }


        [BindProperty] public InputModel Input { get; set; }

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

        public async Task<IActionResult> OnGet(int adId)
        {
            CategoryList = await _adHandler.GetCategoriesList();

            Input = new InputModel();
            AdToEdit = await _adHandler.Get(adId);

            var seller = await _userManager.GetUserAsync(User);
            if (AdToEdit.TorgetUser.Id != seller.Id) return RedirectToPage("MinaAnnonser");


            Input.Title = AdToEdit.Title;
            Input.Description = AdToEdit.Description;
            Input.Price = AdToEdit.Price;
            Input.Category = AdToEdit.Category.Name;
            //STUDY Lägg till så att en kan ändra bilder , kategori(BUG) och taggar

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int adId)
        {
            if (ModelState.IsValid)
            {
                //TODO Byt till följade:
                //var seller = await _userManager.GetUserAsync(User);

                AdToEdit = await _adHandler.Get(adId);

                AdToEdit.Title = Input.Title;
                AdToEdit.Description = Input.Description;
                AdToEdit.Price = Input.Price;
                AdToEdit.DateUpdated = DateTime.Now;


                await _adHandler.Update(AdToEdit);

                return RedirectToPage("/MinaAnnonser", new {area = "Konto"});
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int adId)
        {
            try
            {
                await _adHandler.Delete(adId);
                return RedirectToPage("/AnnonsRaderad", new {area = "Konto"});
            }
            catch
            {
                ErrorOccurred = true;
                Input = new InputModel();
                AdToEdit = await _adHandler.Get(adId);

                Input.Title = AdToEdit.Title;
                Input.Description = AdToEdit.Description;
                Input.Price = AdToEdit.Price;
                Input.Category = AdToEdit.Category.Name;

                return Page();
            }
        }

        public async Task<IActionResult> OnPostSold(int adId)
        {
            await _adHandler.MarkAsSold(adId);
            return RedirectToPage("/MinaAnnonser", new {area = "Konto"});
        }
    }
}