using System.ComponentModel.DataAnnotations;

namespace Torget__Blocket_klon_.Data.Models;

public class Bild
{
    public int Id { get; set; }
    [Required] public string Url { get; set; }
}