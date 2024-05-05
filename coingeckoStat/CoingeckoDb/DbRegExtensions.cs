using Microsoft.Extensions.DependencyInjection;

namespace CoingeckoDb;

public static class DbRegExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection services, string ConnectionString)
    {
        services.AddSingleton<DBConfig>();
        
        EntityFrameworkServiceCollectionExtensions.AddDbContext<AppDbContext>(services);
        return services;
    }

    public static IServiceProvider InitDb(this IServiceProvider serviceProvider)
    {
        using IServiceScope serviceScope = serviceProvider.CreateScope();
        serviceScope.ServiceProvider.GetRequiredService<AppDbContext>().Init();
        return serviceProvider;
    }
}
