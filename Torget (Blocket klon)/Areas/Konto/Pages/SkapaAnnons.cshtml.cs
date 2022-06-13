using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

namespace Torget__Blocket_klon_.Areas.Konto.Pages;

[Authorize]
public class SkapaAnnonsModel : PageModel
{
    private readonly AdHandler _adHandler;

    private readonly UserManager<TorgetUser> _userManager;
    private IWebHostEnvironment _webenvironment;


    public SkapaAnnonsModel(UserManager<TorgetUser> userManager, AdHandler adHandler, IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _adHandler = adHandler;
        _webenvironment = environment;
    }

    [BindProperty] public InputModel Input { get; set; }

    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);

        var imagePaths = await AddImages(Input.AdImages);


        var newTorgetAd = new TorgetAd
        {
            Title = Input.Titel,
            Category = new TorgetCategory() {Name = Input.Kategori},
            Description = Input.Beskrivning,
            Price = Input.Pris,
            DatePosted = DateTime.Now,
            TorgetUser = user,
            AdImages = imagePaths
        };

        var createdTorgetAd = await _adHandler.Create(newTorgetAd);


        return Redirect("/Annonser/annons/" +
                        $"{createdTorgetAd.Id}"); //returna till en preview sida?
    }

    public async Task<List<AdImage>> AddImages(List<IFormFile> postedFiles)
    {
        var fileUploadPath = _webenvironment.WebRootPath + "\\AdImageUploads";


        if (!Directory.Exists(fileUploadPath)) Directory.CreateDirectory(fileUploadPath);

        var uploadedFiles = new List<string>();

        foreach (var postedFile in postedFiles)
            if (postedFile.Length > 0)
            {
                var file = Path.Join(fileUploadPath, postedFile.FileName);

                var pathSplit = file.Split("\\");

                var URLPath = "/" + pathSplit[^2] + "/" + pathSplit[^1];


                await using (var stream = new FileStream(file, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }

                uploadedFiles.Add(URLPath);
            }

        var returnList = CreateAdImageList(uploadedFiles);

        return returnList;
    }

    public List<AdImage> CreateAdImageList(List<string> uploadedFiles)
    {
        var urlList = new List<AdImage>();

        foreach (var uploadedFile in uploadedFiles) urlList.Add(new AdImage {Url = uploadedFile});

        return urlList;
    }

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
        public List<IFormFile>? AdImages { get; set; }
    }
}