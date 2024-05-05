using Microsoft.EntityFrameworkCore;

namespace CoingeckoDb;

public class AppDbContext : DbContext
{
    private readonly DBConfig config;
    public AppDbContext(DBConfig config)
    {
        this.config = config;
    }

    public DbSet<Entities.Coin> Coin { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(config.ConnectionString, x =>
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
