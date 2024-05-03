namespace CoingeckoHttpClient;

public static class CoingeckoHttpClientExtensions
{
    public static IServiceCollection AddCoingeckoHttpClient(this IServiceCollection services) 
    {
        services.AddHttpClient<CoingeckoHttpClient>();

        return services;
    }
}
