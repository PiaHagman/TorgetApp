using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.ModelsNotInDb;

public class SearchQuery
{
    public string? SearchWords { get; set; }
    public string? Category { get; set; }
    public int? PriceLowest { get; set; }
    public int? PriceHighest { get; set; }
    public List<Tag>? Tags { get; set; }

    public IQueryable<TorgetAd> AddSearchQueryToQuery(IQueryable<TorgetAd> queryable)
    {
        if (SearchWords is not null)
        {
            var wordArray = SearchWords.Split(" ");
            //TODO: Snacka om hur vi vill hantera sökning på sökord. Ska hela strängen stämma eller ska ett ord bara kunna stämma (funkar så nu)?

            queryable = queryable.Where(CreateExpressionForTitleContains(wordArray)).AsQueryable();
        }

        if (Category is not null)
            queryable = queryable.Where(a => a.Category == Category);


        if (Tags is not null)
            queryable = queryable.Where(CreateExpressionForTagsContains(Tags)).AsQueryable();


        if (PriceLowest is not null)
            queryable = queryable.Where(a => a.Price >= PriceLowest);


        if (PriceHighest is not null)
            queryable = queryable.Where(a => a.Price <= PriceHighest);

        return queryable;
    }

    private static Expression<Func<TorgetAd, bool>> CreateExpressionForTitleContains(string[] words)
    {
        Expression<Func<TorgetAd, bool>> predicate = (torgetAd) => false;

        predicate = words
            .Aggregate(predicate, (current, word) =>
                Or(current, a => a.Title.Contains(word)));
        return predicate;
    }


    private static Expression<Func<TorgetAd, bool>> CreateExpressionForTagsContains(List<Tag> tags)
    {
        Expression<Func<TorgetAd, bool>> predicate = (torgetAd) => false;

        predicate = tags
            .Aggregate(predicate, (current, tag) =>
                Or(current, a => a.Tags.Any(t => t.TagName == tag.TagName)));
        return predicate;
    }

    private static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> where1,
        Expression<Func<T, bool>> where2)
    {
        InvocationExpression invocationExpression = Expression.Invoke(where2, where1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(where1.Body, invocationExpression),
            where1.Parameters);
    }
}