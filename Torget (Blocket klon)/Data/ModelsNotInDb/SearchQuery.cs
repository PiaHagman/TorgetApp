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
    public List<Tag> Tags { get; set; }

    public IQueryable<TorgetAd> AddSearchQueryToQuery(IQueryable<TorgetAd> queryable)
    {
        if (SearchWords is not null)
        {
            var wordArray = SearchWords.Split(" ");

            queryable = queryable.Where(CreateExpression(wordArray)).AsQueryable();
        }

        return queryable;
    }

    public Func<TorgetAd, bool> CreateExpression(string[] words)
    {
        Expression<Func<TorgetAd, bool>> predicate = (torgetAd) => false;

        predicate = words
            .Aggregate(predicate, (current, word) =>
                Or(current, a => a.Title.Contains(word)));
        return predicate.Compile();
    }

    private static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> where1,
        Expression<Func<T, bool>> where2)
    {
        InvocationExpression invocationExpression = Expression.Invoke(where2, where1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(where1.Body, invocationExpression),
            where1.Parameters);
    }
}