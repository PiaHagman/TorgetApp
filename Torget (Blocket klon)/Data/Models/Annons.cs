using System.ComponentModel.DataAnnotations;

namespace Torget__Blocket_klon_.Data.Models;

public class Annons
{
    [Required]
    public string Titel { get; set; }
    [Required]
    public string Kategori { get; set; }
    [Required]
    public string Beskrivning { get; set; }
    [Required]
    public int Pris { get; set; }

    public DateTime DatumUpplagd { get; } = DateTime.Now;
    public DateTime DatumUppdaterad { get; set; }
    [Required]
    public TorgetUser TorgetUser { get; set; }
    public List<string> Bilder { get; set; }
    public List<string> Taggar { get; set; }
}