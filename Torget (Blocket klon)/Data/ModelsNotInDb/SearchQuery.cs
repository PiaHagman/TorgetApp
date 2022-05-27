using System.Linq.Expressions;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.ModelsNotInDb;

//TODO: Make sure to do checks against normalized strings.
public class SearchQuery
{
    public string? SearchWords { get; set; }
    public string? Category { get; set; }
    public int? PriceLowest { get; set; }
    public int? PriceHighest { get; set; }
    public List<Tag>? Tags { get; set; }

    public IQueryable<TorgetAd> AddSearchQuery(IQueryable<TorgetAd> queryable)
    {
        queryable = TryAddSearchWordsToQuery(queryable);
        queryable = TryAddTagsToQuery(queryable);

        if (Category is not null)
            queryable = queryable.Where(a => a.Category == Category);

        if (PriceLowest is not null)
            queryable = queryable.Where(a => a.Price >= PriceLowest);

        if (PriceHighest is not null)
            queryable = queryable.Where(a => a.Price <= PriceHighest);

        return queryable;
    }

    #region PrivateMethods

    //TODO: Snacka om hur vi vill hantera sökning på sökord. Ska hela strängen stämma eller ska ett ord bara kunna stämma (funkar så nu)?
    private IQueryable<TorgetAd> TryAddSearchWordsToQuery(IQueryable<TorgetAd> queryable)
    {
        return SearchWords is null ? queryable : queryable.Where(CreateExpressionForTitle());
    }

    private IQueryable<TorgetAd> TryAddTagsToQuery(IQueryable<TorgetAd> queryable)
    {
        return Tags is null ? queryable : queryable.Where(CreateExpressionForTags());
    }

    private Expression<Func<TorgetAd, bool>> CreateExpressionForTitle()
    {
        var words = SearchWords.Split(" ");

        Expression<Func<TorgetAd, bool>> expression = (torgetAd) => false;

        expression = words
            .Aggregate(expression, (currentExpression, nextWord) =>
                CreateOrExpression(currentExpression, a =>
                    a.Title.Contains(nextWord)));

        return expression;
    }

    private Expression<Func<TorgetAd, bool>> CreateExpressionForTags()
    {
        Expression<Func<TorgetAd, bool>> expression = (torgetAd) => false;

        expression = Tags
            .Aggregate(expression, (currentExpression, nextTag) =>
                CreateOrExpression(currentExpression, a =>
                    a.Tags.Any(t => t.TagName == nextTag.TagName)));

        return expression;
    }

    private static Expression<Func<T, bool>> CreateOrExpression<T>(Expression<Func<T, bool>> where1,
        Expression<Func<T, bool>> where2)
    {
        InvocationExpression invocationExpression = Expression.Invoke(where2, where1.Parameters.Cast<Expression>());

        Expression<Func<T, bool>> returnExpression = Expression
            .Lambda<Func<T, bool>>(Expression
                .OrElse(where1.Body, invocationExpression), where1.Parameters);

        return returnExpression;
    }

    #endregion
}