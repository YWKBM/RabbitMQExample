using CoingeckoLogic.RabbitMQ;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CoingeckoLogic;

public class CoingeckoService
{
    private readonly CoingeckoHttpClient.CoingeckoHttpClient httpClient;
    private readonly IRabbitMqService rabbitMqService;
    private readonly IConnectionMultiplexer cache;

    public CoingeckoService(CoingeckoHttpClient.CoingeckoHttpClient httpClient, IRabbitMqService rabbitMqService, IConnectionMultiplexer cache) 
    {
        this.httpClient = httpClient;
        this.rabbitMqService = rabbitMqService;
        this.cache = cache;
    }


    // Save data to Redis
    public async Task GetCoinsData()
    {
        var coins = await httpClient.GetCoinsList(false);

        var redis = cache.GetDatabase();

        foreach (var coin in coins) 
        {
            var jsonCoin = JsonConvert.SerializeObject(coin);

            await redis.ListRightPushAsync("coin_stack", jsonCoin);
        }

    }

    // Every Five Seconds
    public async Task ProcessCoinsData() 
    {
        var redis = cache.GetDatabase();

        var poppedCoin = await redis.ListLeftPopAsync("coin_stack");

        if (poppedCoin.HasValue)
        {

#pragma warning disable CS8604
            var coin = JsonConvert.DeserializeObject<CoingeckoHttpClient.DTO.GetCoinsListResponseData>(poppedCoin);

            if (coin is null) 
            {
                return;
            }
            var coinPrice = await httpClient.GetCoinPrice(coin.Id, "rub");

            var message = new Messages.CoinMessage
            {
                CoinId = coin.Id,
                Currency = coinPrice.Values.First().Keys.First(),
                Price = coinPrice.Values.First().Values.First()
            };

            rabbitMqService.SendMessage(message);
            Console.WriteLine($"Sent to queue coinId: {message.CoinId}");
        }
    }
}
