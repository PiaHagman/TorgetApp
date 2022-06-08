using System.Linq.Expressions;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.ModelsNotInDb;

public class SearchQuery
{
    public string? SearchWords { get; set; }
    public string? Category { get; set; }
    public int? PriceLowest { get; set; }
    public int? PriceHighest { get; set; }
    public List<Tag>? Tags { get; set; }

    /// <summary>
    ///     Adds the searchQuery to the specific queryable.
    ///     See the added comment to get a proper understanding.
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns>The <paramref name="queryable" /> with the searchQuery added.</returns>
    public IQueryable<TorgetAd> AddSearchQuery(IQueryable<TorgetAd> queryable)
    {
        if (SearchWords is not null)
            queryable = queryable.Where(CreateAOrExpressionChainForTitle());

        if (Tags is not null)
            queryable = queryable.Where(CreateAOrExpressionChainForTags());

        if (Category is not null)
            queryable = queryable.Where(a => a.Category.Name.ToUpper() == Category.ToUpper());

        if (PriceLowest is not null)
            queryable = queryable.Where(a => a.Price >= PriceLowest);

        if (PriceHighest is not null)
            queryable = queryable.Where(a => a.Price <= PriceHighest);

        return queryable;
    }

    #region PrivateMethods

    // This method chaining will create an expression to be used in the where above.
    // I have used a chain to symbolize the "chaining" of expressions.
    // eg. a => a.Title == b || a.Title == c || a.Title == d || a.Title == e
    private Expression<Func<TorgetAd, bool>> CreateAOrExpressionChainForTitle()
    {
        var titleWords = SearchWords.Split(" ");

        Expression<Func<TorgetAd, bool>> FilterBuilder(string word)
        {
            return ad => ad.Title.ToUpper().Contains(word.ToUpper());
        }

        var returnExpression = BuildAOrExpressionChain(titleWords, FilterBuilder);

        return returnExpression;
    }

    private Expression<Func<TorgetAd, bool>> CreateAOrExpressionChainForTags()
    {
        var tagNames = Tags.Select(item => item.TagName).ToArray();

        Expression<Func<TorgetAd, bool>> FilterBuilder(string tagName)
        {
            return ad => ad.Tags.Any(tag => tag.TagName.ToUpper() == tagName.ToUpper());
        }

        var returnExpression = BuildAOrExpressionChain(tagNames, FilterBuilder);

        return returnExpression;
    }

    private static Expression<Func<TorgetAd, bool>> BuildAOrExpressionChain(string[] filterConstants,
        Func<string, Expression<Func<TorgetAd, bool>>> filterBuilder)
    {
        Expression<Func<TorgetAd, bool>> firstExpression = ad => false;

        var expressionChain = filterConstants
            .Aggregate(firstExpression,
                (chain, filterConstant) => AddAOrChainLink(chain, filterBuilder(filterConstant)));

        return expressionChain;
    }

    private static Expression<Func<TorgetAd, bool>> AddAOrChainLink(Expression<Func<TorgetAd, bool>> chain,
        Expression<Func<TorgetAd, bool>> nextFilter)
    {
        var filterWithChainContext = Expression.Invoke(nextFilter, chain.Parameters);

        chain = Expression.Lambda<Func<TorgetAd, bool>>(Expression
            .OrElse(chain.Body, filterWithChainContext), chain.Parameters);

        return chain;
    }

    #endregion
}