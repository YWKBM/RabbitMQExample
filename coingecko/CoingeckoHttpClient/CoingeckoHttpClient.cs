using System.Text.Json;
using System.Text.Json.Serialization;
using CoingeckoHttpClient.DTO;
using Microsoft.AspNetCore.WebUtilities;

namespace CoingeckoHttpClient;

public class CoingeckoHttpClient
{
    private readonly static JsonSerializerOptions jsonOptions = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };
    private readonly HttpClient httpClient;

    public CoingeckoHttpClient(HttpClient httpClient) 
    {
        this.httpClient = httpClient;
    }

    public async Task<GetCoinsListResponse> GetCoinsList(bool includePlatform)
    {
        var request = new GetCoinsListRequest
        {
            IncludePlatform = includePlatform
        };

        var content = new Dictionary<string, string?>
        {
            {"include_platform", request.IncludePlatform.ToString()}
        };

        var requestUri = QueryHelpers.AddQueryString("https://api.coingecko.com/api/v3/coins/list", content);

        Console.WriteLine(requestUri);

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = await httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GetCoinsListResponse>(jsonOptions)
            ?? throw new Exception("Ошибка парсинга ответа от GetCoinsList");

        return result;
    }

    public async Task<GetCoinsPriceResponse> GetCoinPrice(string id, string currency) 
    {

        var content = new Dictionary<string, string?>
        {
            {"ids", id},
            {"vs_currencies", currency}
        };

        var requestUri = QueryHelpers.AddQueryString("https://api.coingecko.com/api/v3/simple/price", content);

        Console.WriteLine(requestUri);

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = await httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GetCoinsPriceResponse>(jsonOptions)
            ?? throw new Exception("Ошибка парсинга ответа от GetCoinsPrice");

        return result;
    }
}