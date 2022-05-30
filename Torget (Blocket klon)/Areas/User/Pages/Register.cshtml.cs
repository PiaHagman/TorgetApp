using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Areas.User.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<TorgetUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        [Required]
        [EmailAddress]
        [BindProperty]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [BindProperty]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon nummer")]
        public string PhoneNumber { get; set; }

        [Required]
        [BindProperty]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Du m�ste ange ett l�senord")]
        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "L�senord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Konfirmation av l�senord �r ett m�ste")]
        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "Upprepa l�senord")]
        [Compare("Password", ErrorMessage = "L�senorden matchar inte.")]
        public string ConfirmPassword { get; set; }

        public RegisterModel(UserManager<TorgetUser> userManager, ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
        }
    }
}