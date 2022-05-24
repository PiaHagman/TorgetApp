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
        throw new NotImplementedException();
    }

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
}

public class AnnonsFinnsEjException : Exception
{
    public AnnonsFinnsEjException() : base("Annonsen finns inte i vår databas")
    {
    }
}