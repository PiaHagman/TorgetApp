using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Torget__Blocket_klon_.Data.Models;

public class TorgetUser : IdentityUser
{
    [Required] public string PostNummer { get; set; }
    public List<Annons>? Annonser { get; set; }
    public List<Annons>? SparadeAnnonser { get; set; }
    public List<Bevakning>? SparadeBevakningar { get; set; }
}