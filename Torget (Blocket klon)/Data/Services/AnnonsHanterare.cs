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

    public async Task<Annons> SkapaNyAnnons(Annons annons)
    {
        var entityEntry = _dbContext.Annonser.Add(annons);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<Annons> HämtaAnnons(int id)
    {
        var annons = await _dbContext.Annonser.Include(a => a.TorgetUser)
            .Include(a => a.Bilder)
            .Include(a => a.Taggar)
            .FirstOrDefaultAsync(a => a.Id == id);

        return annons ?? throw new AnnonsFinnsEjException(); //Förstår vi detta allihopa?
    }

    public async Task<List<Annons>> HämtaAnnonser(SökQuery? sökQuery = null)
    {
        var query = _dbContext.Annonser
            .Include(a => a.TorgetUser)
            .Include(a => a.Bilder)
            .Include(a => a.Taggar)
            .AsQueryable();
        if (sökQuery is not null)
        {
            //Take according to query
        }

        var annonser = await query.ToListAsync();
        return annonser;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<Annons> ÄndraAnnons(int id, Annons uppdateradAnnons)
    {
        var annons = await _dbContext.Annonser.FindAsync(id);
        if (annons == null) throw new AnnonsFinnsEjException();

        annons = uppdateradAnnons;
        var entityEntry = _dbContext.Annonser.Update(annons);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<Annons> MarkeraAnnonsSomSåld(int id)
    {
        var annons = await _dbContext.Annonser.FindAsync(id);
        if (annons == null) throw new AnnonsFinnsEjException();

        annons.Såld = true;
        var entityEntry = _dbContext.Annonser.Update(annons);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    /// <exception cref="AnnonsFinnsEjException"></exception>
    public async Task<Annons> TaBortAnnons(int id)
    {
        var annons = await _dbContext.Annonser.FindAsync(id);
        if (annons == null) throw new AnnonsFinnsEjException();

        var entityEntry = _dbContext.Annonser.Remove(annons);
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