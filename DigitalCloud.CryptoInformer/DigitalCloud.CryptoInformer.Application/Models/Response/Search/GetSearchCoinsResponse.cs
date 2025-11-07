using System.Text.Json.Serialization;

namespace DigitalCloud.CryptoInformer.Application.Models.Response.Search;

public class GetSearchCoinsResponse
{
    [JsonPropertyName("coins")]
    public List<CoinSearchItem> Coins { get; init; } = new();
}

public class CoinSearchItem
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; } 

    [JsonPropertyName("api_symbol")]
    public required string ApiSymbol { get; init; } 

    [JsonPropertyName("symbol")]
    public required string Symbol { get; init; }

    [JsonPropertyName("market_cap_rank")]
    public int? MarketCapRank { get; init; }

    [JsonPropertyName("thumb")]
    public required string Thumb { get; init; } 

    [JsonPropertyName("large")]
    public required string Large { get; init; } 
}