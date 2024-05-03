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
        services.AddScoped<Jobs.ProcessCoinsData>();

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("127.0.0.1:6379,abortConnect=false"));

        return services;
    }

    public static IServiceProvider ConfigureLogic(this IServiceProvider serviceProvider) 
    {
        serviceProvider.UseScheduler(s =>
        {
            s.Schedule<Jobs.GetCoinsDataJob>()
                .EveryMinute()
                .PreventOverlapping("CoinsDataLoad");
            s.Schedule<Jobs.ProcessCoinsData>()
                .EveryFiveSeconds()
                .PreventOverlapping("CoinsProcessing");
        }).OnError(e =>
        {
            Console.WriteLine(e.ToString());
        });

        return serviceProvider;
    }  
}