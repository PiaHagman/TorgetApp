using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Torget__Blocket_klon_.Data.Models;

public class TorgetCategory
{
    private string _name;

    [Key]
    public string Name
    {
        get => _name;
        set => _name = value.ToUpper();
    }

    public List<TorgetAd> Ads { get; set; }
}