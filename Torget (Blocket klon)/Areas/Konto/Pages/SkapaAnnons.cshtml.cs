using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Torget__Blocket_klon_.Areas.Konto.Pages
{

    public class SkapaAnnonsModel : PageModel
    {

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
            public string Pris { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Kategori")]
            public string Kategori { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Taggar")]
            public string Taggar { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Ad Image")]
            public IFormFile? AdImage { get; set; }
        }

        public void OnGet()
        {
        }

        public Task<IActionResult> OnPostAsync()
        {

        }
    }
}
