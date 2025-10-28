using System.Text.Json.Serialization;

namespace DigitalCloud.CryptoInformer.Application.Models.Response;

public class CurrencyInfoResponse
{
    [JsonPropertyName("market_cap_rank")]
    public int MarketCapRank { get; set; }

    [JsonPropertyName("id")]
    public required string CurrencyId { get; set; }

    [JsonPropertyName("symbol")]
    public required string CurrencySymbol { get; set; }

    [JsonPropertyName("name")]
    public required string CurrencyName { get; set; }

    [JsonPropertyName("image")]
    public required string CurrencyIconImageLink { get; set; }

    [JsonPropertyName("current_price")]
    public decimal CurrentPrice { get; set; }

    [JsonPropertyName("market_cap")]
    public decimal MarketCap { get; set; }

    [JsonPropertyName("total_volume")]
    public decimal TotalVolume { get; set; }

    [JsonPropertyName("price_change_percentage_24h_in_currency")]
    public decimal? PriceChangePercentage24h { get; set; }
}