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

    public IQueryable<TorgetAd> AddSearchQuery(IQueryable<TorgetAd> queryable)
    {
        if (SearchWords is not null)
            queryable = queryable.Where(CreateOrExpressionChainForTitle());

        if (Tags is not null)
            queryable = queryable.Where(CreateOrExpressionChainForTags());

        if (Category is not null)
            queryable = queryable.Where(a => a.Category.ToUpper() == Category.ToUpper());

        if (PriceLowest is not null)
            queryable = queryable.Where(a => a.Price >= PriceLowest);

        if (PriceHighest is not null)
            queryable = queryable.Where(a => a.Price <= PriceHighest);

        return queryable;
    }

    #region PrivateMethods

    private Expression<Func<TorgetAd, bool>> CreateOrExpressionChainForTitle()
    {
        var titleWords = SearchWords.Split(" ");

        Expression<Func<TorgetAd, bool>> FilterBuilder(string word) =>
            (ad) => ad.Title.ToUpper().Contains(word.ToUpper());

        var returnExpression = BuildOrExpressionChain(titleWords, FilterBuilder);

        return returnExpression;
    }

    private Expression<Func<TorgetAd, bool>> CreateOrExpressionChainForTags()
    {
        var tagNames = Tags.Select(item => item.TagName).ToArray();

        Expression<Func<TorgetAd, bool>> FilterBuilder(string tagName) =>
            (ad) => ad.Tags.Any(tag => tag.TagName.ToUpper() == tagName.ToUpper());

        var returnExpression = BuildOrExpressionChain(tagNames, FilterBuilder);

        return returnExpression;
    }

    private static Expression<Func<TorgetAd, bool>> BuildOrExpressionChain(string[] filterConstants,
        Func<string, Expression<Func<TorgetAd, bool>>> filterBuilder)
    {
        Expression<Func<TorgetAd, bool>> firstExpression = (ad) => false;

        var expressionChain = filterConstants
            .Aggregate(firstExpression,
                (chain, filterConstant) => AddOrChainLink(chain, filterBuilder(filterConstant)));

        return expressionChain;
    }

    private static Expression<Func<TorgetAd, bool>> AddOrChainLink(Expression<Func<TorgetAd, bool>> chain,
        Expression<Func<TorgetAd, bool>> nextFilter)
    {
        var filterWithChainContext = Expression.Invoke(nextFilter, chain.Parameters);

        chain = Expression.Lambda<Func<TorgetAd, bool>>(Expression
            .OrElse(chain.Body, filterWithChainContext), chain.Parameters);

        return chain;
    }

    #endregion
}