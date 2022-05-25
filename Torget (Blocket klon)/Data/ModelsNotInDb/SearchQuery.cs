using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.ModelsNotInDb;

public class SearchQuery
{
    public string? SearchWords { get; set; }
    public string? Category { get; set; }
    public int? PriceLowest { get; set; }
    public int? PriceHighest { get; set; }
    public List<Tag> Tags { get; set; }

    public IQueryable<TorgetAd> AddSearchQueryToQuery(IQueryable<TorgetAd> queryable)
    {
        if (SearchWords is not null)
        {
            queryable = queryable.Where(a => a.Title.Contains(SearchWords));
        }

        return queryable;
    }
}