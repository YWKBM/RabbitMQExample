using System.Text.Json.Serialization;

namespace CoingeckoHttpClient.DTO;

public record GetCoinsPriceRequest
{
    public required string Id { get; set; }

    public required string Currency { get; set; }
}