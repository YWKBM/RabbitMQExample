namespace CoingeckoHttpClient;

public static class CoingeckoHttpClientExtensions
{
    public static IServiceCollection AddCoingeckoHttpClient(this IServiceCollection services) 
    {
        services.AddHttpClient<CoingeckoHttpClient>()
            .AddStandardResilienceHandler(config => 
            {
                config.Retry.BackoffType = Polly.DelayBackoffType.Exponential;  
            });

        return services;
    }
}
