using Humanizer;
using Microsoft.EntityFrameworkCore;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.ModelsNotInDb;

namespace Torget__Blocket_klon_.Data.Services;

public class AdHandler
{
    private readonly TorgetDbContext _dbContext;

    public AdHandler(TorgetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TorgetAd> Create(TorgetAd ad)
    {
        var category = await _dbContext.TorgetCategories.FirstOrDefaultAsync(c => c.Name == ad.Category.Name);

        if (category is null) throw new CategoryDoesNotExistException();

        ad.Category = category;

        var entityEntry = _dbContext.TorgetAds.Add(ad);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    /// <exception cref="AdDoesNotExistException"></exception>
    public async Task<TorgetAd> Get(int id)
    {
        var ad = await _dbContext.TorgetAds
            .Include(a => a.TorgetUser)
            .Include(a => a.AdImages)
            .Include(a => a.Tags)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == id);

        return ad ?? throw new AdDoesNotExistException();
    }

    public async Task<List<TorgetAd>> GetUserAds(string userId)
    {
        var userAds = await _dbContext.TorgetAds
            .Include(a => a.TorgetUser)
            .Include(a => a.AdImages)
            .Include(a => a.Tags)
            .Include(a => a.Category)
            .Where(a => a.TorgetUser.Id == userId)
            .ToListAsync();

        return userAds;
    }

    public async Task<List<TorgetAd>> GetList(SearchQuery? searchQuery = null)
    {
        var query = _dbContext.TorgetAds
            .Include(a => a.TorgetUser)
            .Include(a => a.AdImages)
            .Include(a => a.Tags) //.AsSplitQuery() Behövs? Läs på om.
            .Include(a => a.Category)
            .AsQueryable();

        if (searchQuery is not null)
            query = searchQuery.AddSearchQuery(query);

        var ads = await query.ToListAsync();

        return ads;
    }

    /// <exception cref="AdDoesNotExistException"></exception>
    public async Task<TorgetAd> Update(TorgetAd updatedAd)
    {
        _dbContext.Attach(updatedAd);
        await _dbContext.SaveChangesAsync();

        return updatedAd;
    }

    /// <exception cref="AdDoesNotExistException"></exception>
    public async Task<TorgetAd> MarkAsSold(int id)
    {
        var ad = new TorgetAd() {Id = id};
        _dbContext.Attach(ad);
        ad.Sold = true;

        await _dbContext.SaveChangesAsync();

        return ad;
    }

    /// <exception cref="AdDoesNotExistException"></exception>
    public async Task<TorgetAd> Delete(int id)
    {
        var ad = await _dbContext.TorgetAds
            .Include(a => a.AdImages)
            .Include(a => a.Tags)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (ad == null) throw new AdDoesNotExistException();

        var entityEntry = _dbContext.TorgetAds.Remove(ad);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<List<string>> GetCategoriesList()
    {
        var categories = await _dbContext.TorgetCategories
            .ToListAsync();

        var categoryList = new List<string>();

        foreach (var category in categories) categoryList.Add(category.Name.ToLower().Humanize(LetterCasing.Title));

        return categoryList;
    }
}

public class CategoryDoesNotExistException : Exception
{
    public CategoryDoesNotExistException() : base("Ogiltig kategori, fulspel?")
    {
    }
}

public class AdDoesNotExistException : Exception
{
    public AdDoesNotExistException() : base("Annonsen finns inte i vår databas")
    {
    }
}