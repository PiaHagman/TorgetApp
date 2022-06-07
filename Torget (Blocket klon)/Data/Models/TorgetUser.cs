using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Torget__Blocket_klon_.Data.Models;

public class TorgetUser : IdentityUser
{
    [Required] public string ZipCode { get; set; }
    public string County { get; set; }
    public List<TorgetAd>? TorgetAds { get; set; }
    public List<TorgetAd>? SavedAds { get; set; }
    public List<AdSearch>? SavedSearches { get; set; }
}