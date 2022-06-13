using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.User.Pages;

[AllowAnonymous]
public class RegistreraModel : PageModel
{
    private readonly UserManager<TorgetUser> _userManager;
    private readonly ZipCodeHandler _zipCodeHandler;

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
    [MaxLength(5)]
    [MinLength(5, ErrorMessage = "Postkoden måste innehålla 5 siffror utan mellanrum.")]
    [Display(Name = "Postnummer")]
    public string ZipCode { get; set; }

    [Display(Name = "Ort")] [BindProperty] public string? County { get; set; }

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

    public RegistreraModel(UserManager<TorgetUser> userManager, ZipCodeHandler zipCodeHandler)
    {
        _userManager = userManager;
        _zipCodeHandler = zipCodeHandler;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            var zipCodeResult = await _zipCodeHandler.GetZipCodeInformation(ZipCode);
            County = zipCodeResult.County;
        }
        catch (ZipCodeNotValidException e)
        {
            ModelState.AddModelError("ZipCodeError", e.Message);
            return Page();
        }

        TorgetUser user = new()
        {
            Email = Email,
            UserName = Email,
            PhoneNumber = PhoneNumber,
            ZipCode = ZipCode,
            County = County
        };

        var result = await _userManager.CreateAsync(user, Password);

        if (result.Succeeded)
            return RedirectToPage("/Index"); //Redirect to Logga in

        foreach (var error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return Page();
    }
}