using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.Services;

public class TorgetDbContext : IdentityDbContext<TorgetUser>
{
    public TorgetDbContext(DbContextOptions<TorgetDbContext> options) : base(options)
    {
    }

    public DbSet<Annons> Annonser { get; set; }
    public DbSet<Bevakning> Bevakningar { get; set; }
    public DbSet<Bild> Bilder { get; set; }
    public DbSet<Tag> Taggar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder
                .UseSqlServer(@$"Server=(localdb)\MSSQLLocalDB;Database=TorgetDb");
    }
}