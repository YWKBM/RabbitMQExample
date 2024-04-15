using System.Text.Json.Serialization;

namespace CoingeckoHttpClient.DTO;

public class GetCoinsListResponse : List<GetCoinsListResponseData>
{

}

public record GetCoinsListResponseData
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;


    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;
}
