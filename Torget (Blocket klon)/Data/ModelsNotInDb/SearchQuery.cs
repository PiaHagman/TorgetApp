using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.ModelsNotInDb;

public class SearchQuery
{
    public string? SökOrd { get; set; }
    public string? Kategori { get; set; }
    public int? PrisLägsta { get; set; }
    public int? PrisHögsta { get; set; }
    public List<Tag> Taggar { get; set; }
}