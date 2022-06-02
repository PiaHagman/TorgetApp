using System.ComponentModel.DataAnnotations;

namespace Torget__Blocket_klon_.Data.Models;

public class AdImage
{
    public int Id { get; set; }
    [Required] public string Url { get; set; }
}