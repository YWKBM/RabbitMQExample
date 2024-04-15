using CoingeckoLogic.RabbitMQ;

namespace CoingeckoLogic;

public class CoingeckoService
{
    private readonly CoingeckoHttpClient.CoingeckoHttpClient httpClient;
    private readonly IRabbitMqService rabbitMqService;

    public CoingeckoService(CoingeckoHttpClient.CoingeckoHttpClient httpClient, IRabbitMqService rabbitMqService) 
    {
        this.httpClient = httpClient;
        this.rabbitMqService = rabbitMqService;
    }


    public async Task GetCoinsData()
    {
        var coins = await httpClient.GetCoinsList(false);

        // Took 4 as example
        var coinsIds = coins.Select(p => p.Id).Take(4).ToArray();

        foreach (var coinId in coinsIds)
        {
            var coinData = await httpClient.GetCoinPrice(coinId, "rub");

            var message = new Messages.CoinMessage
            {
                CoinId = coinData.Keys.First(),
                Currency = coinData.Values.First().Keys.First(),
                Price = coinData.Values.First().Values.First(),
                DateTime = DateTimeOffset.UtcNow,
            };

            rabbitMqService.SendMessage(message);

            Console.WriteLine($"Send to Queue: {message.CoinId}");

            Thread.Sleep(500);
        }
    }
}
