using Microsoft.AspNetCore.Identity;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.Services;

public class Database
{
    private readonly TorgetDbContext _dbCtx;
    private readonly UserManager<TorgetUser> _userManager;

    public Database(TorgetDbContext dbCtx, UserManager<TorgetUser> userManager)
    {
        _dbCtx = dbCtx;
        _userManager = userManager;
    }

    public async Task RecreateAndSeedDatabase()
    {
        await _dbCtx.Database.EnsureDeletedAsync();
        await _dbCtx.Database.EnsureCreatedAsync();
        await SeedTestData();
    }

    //TODO: 
    private async Task SeedTestData()
    {
        var user1 = new TorgetUser()
        {
            Email = "user1@email.com",
            UserName = "user1@email.com",
            ZipCode = "44340",
            Id = "43eefa21-9b75-4926-9e1f-d9a878aa5f24"
        };
        var user2 = new TorgetUser()
        {
            Email = "user2@email.com",
            UserName = "user2@email.com",
            ZipCode = "42446",
            Id = "f20ff2b1-a75d-4ce0-a245-6415284391cf"
        };

        await _userManager.CreateAsync(user1, "Passw0rd!");
        await _userManager.CreateAsync(user2, "Passw0rd!");

        var annonser = new List<TorgetAd>()
        {
            new()
            {
                Title = "TorgetAd 1",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non erat quis orci porttitor sodales. Vivamus at diam dignissim, eleifend est id, placerat lacus. Vivamus semper sapien in maximus varius. Sed tincidunt quam quis dui sollicitudin, ac tincidunt purus tempor. Nulla eleifend vitae augue sit amet tincidunt. Aenean fermentum diam.",
                Category = "Cykel",
                Price = 50,
                TorgetUser = user1,
                SavedByUsers = new List<TorgetUser> {user2},
                Tags = new List<Tag> {new() {TagName = "Snabb"}},
                AdImages = new List<AdImage> {new() {Url = "Bild1Annons1Url"}}
            },
            new()
            {
                Title = "TorgetAd 2",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non erat quis orci porttitor sodales. Vivamus at diam dignissim, eleifend est id, placerat lacus. Vivamus semper sapien in maximus varius. Sed tincidunt quam quis dui sollicitudin, ac tincidunt purus tempor. Nulla eleifend vitae augue sit amet tincidunt. Aenean fermentum diam.",
                Category = "Dator",
                Price = 100,
                TorgetUser = user1,
                SavedByUsers = new List<TorgetUser> {user2},
                Tags = new List<Tag> {new() {TagName = "Snabb"}, new() {TagName = "Snygg"}},
                AdImages = new List<AdImage> {new() {Url = "Bild1Annons2Url"}, new() {Url = "Bild2Annons2Url"}}
            },
            new()
            {
                Title = "TorgetAd 3",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non erat quis orci porttitor sodales. Vivamus at diam dignissim, eleifend est id, placerat lacus. Vivamus semper sapien in maximus varius. Sed tincidunt quam quis dui sollicitudin, ac tincidunt purus tempor. Nulla eleifend vitae augue sit amet tincidunt. Aenean fermentum diam.",
                Category = "Bil",
                Price = 150,
                TorgetUser = user2,
                Tags = new List<Tag> {new() {TagName = "Ferrari"}},
                AdImages = new List<AdImage> {new() {Url = "Bild1Annons3Url"}}
            }
        };

        _dbCtx.TorgetAds.AddRange(annonser);
        await _dbCtx.SaveChangesAsync();
    }
}