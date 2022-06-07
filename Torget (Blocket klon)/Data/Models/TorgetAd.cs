using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torget__Blocket_klon_.Data.Models;

public class TorgetAd
{
    public TorgetAd()
    {
        DatePosted = DateTime.Now;
    }

    public int Id { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Category { get; set; }
    [Required] public string Description { get; set; }

    [Required]
    [Column(TypeName = "smallmoney")]
    public int Price { get; set; }

    public bool Sold { get; set; } = false;
    public DateTime DatePosted { get; set; }
    public DateTime DateUpdated { get; set; }

    [InverseProperty("TorgetAds")] public TorgetUser TorgetUser { get; set; }

    [InverseProperty("SavedAds")]
    public List<TorgetUser>? SavedByUsers { get; set; } 
    public List<AdImage> AdImages { get; set; }
    public List<Tag> Tags { get; set; }
}