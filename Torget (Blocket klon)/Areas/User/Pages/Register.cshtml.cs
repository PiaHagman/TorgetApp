using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Areas.User.Pages;

public class RegisterModel : PageModel
{
    private readonly UserManager<TorgetUser> _userManager;
    private readonly ILogger<RegisterModel> _logger;

    [Required(ErrorMessage = "Du måste ange en email")]
    [EmailAddress]
    [BindProperty]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Du måste ange ett telefon nummer")]
    [BindProperty]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Telefon nummer")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Du måste ange en postkod")]
    [BindProperty]
    [DataType(DataType.PostalCode)]
    [Display(Name = "Postnummer")]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "Du måste ange ett lösenord")]
    [BindProperty]
    [DataType(DataType.Password)]
    [Display(Name = "Lösenord")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Konfirmation av lösenord är ett måste")]
    [BindProperty]
    [DataType(DataType.Password)]
    [Display(Name = "Upprepa lösenord")]
    [Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
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