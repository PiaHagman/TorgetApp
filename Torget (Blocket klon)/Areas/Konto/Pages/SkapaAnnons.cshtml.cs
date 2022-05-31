using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{

    public class SkapaAnnonsModel : PageModel
    {

        private readonly UserManager<TorgetUser> _userManager;
        private readonly AdHandler _adHandler;
        private IHostEnvironment _environment;


        public SkapaAnnonsModel(UserManager<TorgetUser> userManager, AdHandler adHandler, IHostEnvironment environment)
        {
            _userManager = userManager;
            _adHandler = adHandler;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Rubrik")]
            public string Titel { get; set; }

            [Required]
            [DataType(DataType.MultilineText)]
            [Display(Name = "Beskrivning")]
            public string Beskrivning { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "Pris")]
            public int Pris { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Kategori")]
            public string Kategori { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Ad Image")]
            public IFormFile? AdImage { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByIdAsync(
                "43eefa21-9b75-4926-9e1f-d9a878aa5f24"); //Tillfällig user.

            var imagePath = await addImage();

            var newTorgetAd = new TorgetAd
            {
                Title = Input.Titel,
                Category = Input.Kategori,
                Description = Input.Beskrivning,
                Price = Input.Pris,
                DatePosted = DateTime.Now,
                TorgetUser = user,
                AdImages = new List<AdImage> { new AdImage { Url = imagePath } },

            };


            await _adHandler.Create(newTorgetAd);

            return Page(); //returna till en preview sida?
        }

        public async Task<string> addImage()
        {
            var file = Path.Join("~/uploads/", Input.AdImage.FileName);
            await using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Input.AdImage.CopyToAsync(fileStream);
            }

            return file;
        }
    }
}
