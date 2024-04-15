namespace CoingeckoLogic.Messages;

public record CoinMessage
{
    public required string CoinId { get; set; }

    public required string Currency { get; set; }   

    public decimal Price { get; set; }

    public DateTimeOffset DateTime { get; set; }
}

