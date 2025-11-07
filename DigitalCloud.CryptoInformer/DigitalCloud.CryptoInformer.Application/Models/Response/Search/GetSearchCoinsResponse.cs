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
    public string Id { get; init; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    [JsonPropertyName("api_symbol")]
    public string ApiSymbol { get; init; } = null!;

    [JsonPropertyName("symbol")]
    public string Symbol { get; init; } = null!;

    [JsonPropertyName("market_cap_rank")]
    public int? MarketCapRank { get; init; }

    [JsonPropertyName("thumb")]
    public string Thumb { get; init; } = null!;

    [JsonPropertyName("large")]
    public string Large { get; init; } = null!;
}