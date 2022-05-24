using Microsoft.AspNetCore.Identity;

namespace Torget__Blocket_klon_.Data.Models;

public class TorgetUser : IdentityUser
{
    public string PostNummer { get; set; }
    public List<Annons> Annonser { get; set; }
    public List<Annons> SparadeAnnonser { get; set; }
    public List<string> SparadeBevakningar { get; set; }
}