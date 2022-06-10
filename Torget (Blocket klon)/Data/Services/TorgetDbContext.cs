using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Torget__Blocket_klon_.Data.Models;

namespace Torget__Blocket_klon_.Data.Services;

public class TorgetDbContext : IdentityDbContext<TorgetUser>
{
    public TorgetDbContext(DbContextOptions<TorgetDbContext> options) : base(options)
    {
    }

    public DbSet<TorgetAd> TorgetAds { get; set; }
    public DbSet<AdSearch> AdSearches { get; set; }
    public DbSet<AdImage> AdImages { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<TorgetCategory> TorgetCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TorgetDb");
    }
}