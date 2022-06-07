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

    private async Task SeedTestData()
    {
        var user1 = new TorgetUser()
        {
            Email = "user1@email.com",
            UserName = "user1@email.com",
            ZipCode = "44340",
            County = "Lerum",
            PhoneNumber = "0766-210910",
            Id = "43eefa21-9b75-4926-9e1f-d9a878aa5f24"
        };
        var user2 = new TorgetUser()
        {
            Email = "user2@email.com",
            UserName = "user2@email.com",
            ZipCode = "42446",
            County = "Göteborg",
            PhoneNumber = "0766-210910",
            Id = "f20ff2b1-a75d-4ce0-a245-6415284391cf"
        };

        await _userManager.CreateAsync(user1, "Passw0rd!");
        await _userManager.CreateAsync(user2, "Passw0rd!");

        var annonser = new List<TorgetAd>()
        {
            new()
            {
                Title = "Färggrann cykel",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non erat quis orci porttitor sodales. Vivamus at diam dignissim, eleifend est id, placerat lacus. Vivamus semper sapien in maximus varius. Sed tincidunt quam quis dui sollicitudin, ac tincidunt purus tempor. Nulla eleifend vitae augue sit amet tincidunt. Aenean fermentum diam.",
                Category = "Cykel",
                Price = 1200,
                TorgetUser = user1,
                SavedByUsers = new List<TorgetUser> {user2},
                Tags = new List<Tag> {new() {TagName = "Snabb"}, new() {TagName = "Snygg"}},
                AdImages = new List<AdImage>
                {
                    new() {Url = "../../images/example-pic.png"}, new() {Url = "../../images/example-pic2.png"},
                    new() {Url = "../../images/example-pic.png"}, new() {Url = "../../images/example-pic.png"}
                }
            },
            new()
            {
                Title = "Sötaste katten",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non erat quis orci porttitor sodales. Vivamus at diam dignissim, eleifend est id, placerat lacus. Vivamus semper sapien in maximus varius. Sed tincidunt quam quis dui sollicitudin, ac tincidunt purus tempor. Nulla eleifend vitae augue sit amet tincidunt. Aenean fermentum diam.",
                Category = "Djur",
                Price = 150,
                TorgetUser = user1,
                SavedByUsers = new List<TorgetUser> {user2},
                Tags = new List<Tag> {new() {TagName = "s"}, new() {TagName = "Söt"}},
                AdImages = new List<AdImage>
                {
                    new() {Url = "../../images/cat.jpg"}, new() {Url = "../../images/cat2.jpg"},
                    new() {Url = "../../images/cat3.jpg"}
                }
            },
            new()
            {
                Title = "Stor maskin",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque non erat quis orci porttitor sodales. Vivamus at diam dignissim, eleifend est id, placerat lacus. Vivamus semper sapien in maximus varius. Sed tincidunt quam quis dui sollicitudin, ac tincidunt purus tempor. Nulla eleifend vitae augue sit amet tincidunt. Aenean fermentum diam.",
                Category = "Maskin",
                Price = 15000,
                TorgetUser = user2,
                Tags = new List<Tag> {new() {TagName = "hjullastare"}},
                AdImages = new List<AdImage>
                {
                    new() {Url = "../../images/maskin.jpg"}, new() {Url = "../../images/maskin1.jpg"},
                    new() {Url = "../../images/maskin2.jpg"}
                }
            }
        };

        _dbCtx.TorgetAds.AddRange(annonser);
        await _dbCtx.SaveChangesAsync();
    }
}