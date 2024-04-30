using CoingeckoHttpClient;
using CoingeckoLogic.RabbitMQ;
using Coravel;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace CoingeckoLogic;

public static class LogicRegExtensions
{
    private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

    public static IServiceCollection AddLogic(this IServiceCollection services) 
    {
        services.AddCoingeckoHttpClient();

        services.AddScoped<CoingeckoService>();

        services.AddScoped<IRabbitMqService, RabbitMqService>();

        services.AddScheduler();
        services.AddScoped<Jobs.GetCoinsDataJob>();

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6479"));



        return services;
    }

    public static IServiceProvider ConfigureLogic(this IServiceProvider serviceProvider) 
    {
        serviceProvider.UseScheduler(s =>
        {
            s.Schedule<Jobs.GetCoinsDataJob>()
                .EveryMinute()
                .PreventOverlapping("CoinsDataLoad");
        }).OnError(e =>
        {
            log.Error(e);
            log.Debug(e.StackTrace);
        });

        return serviceProvider;
    }  
}