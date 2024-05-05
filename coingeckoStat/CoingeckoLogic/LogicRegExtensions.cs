using CoingeckoDb;
using CoingeckoLogic.Configs;
using CoingeckoLogic.Consumer;
using Microsoft.Extensions.DependencyInjection;

namespace CoingeckoLogic;

public static class LogicRegExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services) 
    {
        services.AddDb(Configs.Config.Postgres.DATABASE);
        services.AddHostedService<QueueListener>();
        return services;
    }

    public static IServiceProvider ConfigureLogic(this IServiceProvider serviceProvider) 
    {
        serviceProvider.InitDb();

        return serviceProvider;
    }
}
