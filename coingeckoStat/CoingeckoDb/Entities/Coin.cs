using System.Text.Json.Serialization;

namespace CoingeckoDb.Entities;

public record Coin 
{
    public int Id { get; set; }    

    [JsonPropertyName("CoinId")]
    public string CoinId { get; set; } = string.Empty;

    [JsonPropertyName("Price")]
    public required decimal Price { get; set; }

    [JsonPropertyName("Currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("DateTime")]
    public DateTimeOffset DateTime { get; set; }
}
