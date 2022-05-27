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
            queryable = AddSearchWordsToQuery(queryable, SearchWords);

        if (Category is not null)
            queryable = queryable.Where(a => a.Category == Category);

        if (PriceLowest is not null)
            queryable = queryable.Where(a => a.Price >= PriceLowest);

        if (PriceHighest is not null)
            queryable = queryable.Where(a => a.Price <= PriceHighest);

        if (Tags is not null)
            queryable = AddTagsToQuery(queryable, Tags);

        return queryable;
    }

    #region PrivateMethods

    //TODO: Snacka om hur vi vill hantera sökning på sökord. Ska hela strängen stämma eller ska ett ord bara kunna stämma (funkar så nu)?
    private static IQueryable<TorgetAd> AddSearchWordsToQuery(IQueryable<TorgetAd> queryable, string searchWords)
    {
        var wordArray = searchWords.Split(" ");
        return queryable.Where(CreateExpressionForTitle(wordArray)).AsQueryable();
    }

    private static IQueryable<TorgetAd> AddTagsToQuery(IQueryable<TorgetAd> queryable, IEnumerable<Tag> tags)
    {
        return queryable.Where(CreateExpressionForTags(tags));
    }

    private static Expression<Func<TorgetAd, bool>> CreateExpressionForTitle(string[] words)
    {
        Expression<Func<TorgetAd, bool>> expression = (torgetAd) => false;

        expression = words
            .Aggregate(expression, (currentExpression, nextWord) =>
                CreateOrExpression(currentExpression, a =>
                    a.Title.Contains(nextWord)));

        return expression;
    }

    private static Expression<Func<TorgetAd, bool>> CreateExpressionForTags(IEnumerable<Tag> tags)
    {
        Expression<Func<TorgetAd, bool>> expression = (torgetAd) => false;

        expression = tags
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