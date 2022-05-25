using Microsoft.EntityFrameworkCore;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.ModelsNotInDb;

namespace Torget__Blocket_klon_.Data.Services;

public class AnnonsHanterare
{
    private readonly TorgetDbContext _dbContext;

    public AnnonsHanterare(TorgetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TorgetAd> SkapaNyAnnons(TorgetAd annons)
    {
        var entityEntry = _dbContext.TorgetAds.Add(annons);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<TorgetAd> Get(int id)
    {
        var annons = await _dbContext.TorgetAds.Include(a => a.TorgetUser)
            .Include(a => a.AdImages)
            .Include(a => a.Tags)
            .FirstOrDefaultAsync(a => a.Id == id);

        return annons ?? throw new AnnonsFinnsEjException(); //Förstår vi detta allihopa?
    }

    public async Task<List<TorgetAd>> GetList(SearchQuery? sökQuery = null)
    {
        var query = _dbContext.TorgetAds
            .Include(a => a.TorgetUser)
            .Include(a => a.AdImages)
            .Include(a => a.Tags)
            .AsQueryable();
        if (sökQuery is not null)
        {
            //Take according to query
        }

        var annonser = await query.ToListAsync();
        return annonser;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<TorgetAd> Update(int id, TorgetAd uppdateradAnnons)
    {
        var annons = await _dbContext.TorgetAds.FindAsync(id);
        if (annons == null) throw new AnnonsFinnsEjException();

        annons = uppdateradAnnons;
        var entityEntry = _dbContext.TorgetAds.Update(annons);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<TorgetAd> MarkAsSold(int id)
    {
        var annons = await _dbContext.TorgetAds.FindAsync(id);
        if (annons == null) throw new AnnonsFinnsEjException();

        annons.Sold = true;
        var entityEntry = _dbContext.TorgetAds.Update(annons);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<TorgetAd> Delete(int id)
    {
        var annons = await _dbContext.TorgetAds.FindAsync(id);
        if (annons == null) throw new AnnonsFinnsEjException();

        var entityEntry = _dbContext.TorgetAds.Remove(annons);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }
}

public class AnnonsFinnsEjException : Exception
{
    public AnnonsFinnsEjException() : base("Annonsen finns inte i vår databas")
    {
    }
}