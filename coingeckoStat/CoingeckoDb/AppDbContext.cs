using Microsoft.EntityFrameworkCore;

namespace CoingeckoDb;

public class AppDbContext : DbContext
{
    public AppDbContext() { }

    public DbSet<Entities.Coin> Coin { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=coingecko;Username=postgres;Password=0591", x =>
        {
            x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
        });

        base.OnConfiguring(optionsBuilder);
    }

    public void Init()
    {
        Database.Migrate();
    }
}
