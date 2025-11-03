using System.Text.Json.Serialization;

namespace DigitalCloud.CryptoInformer.Application.Models.Response;

public class GetCoinDetailsResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    [JsonPropertyName("symbol")]
    public string Symbol { get; init; } = null!;

    [JsonPropertyName("market_cap_rank")]
    public int? MarketCapRank { get; init; }

    [JsonPropertyName("image")]
    public CoinImageData? Image { get; init; }

    [JsonPropertyName("market_data")]
    public MarketData? MarketData { get; init; }

    [JsonPropertyName("hashing_algorithm")]
    public string? HashingAlgorithm { get; init; }

    [JsonPropertyName("genesis_date")]
    public string? GenesisDate { get; init; }

    [JsonPropertyName("categories")]
    public string[] Categories { get; init; } = [];

    [JsonPropertyName("links")]
    public CoinLinks? Links { get; init; }

    [JsonPropertyName("tickers")]
    public List<CoinMarketRow> Markets { get; init; } = [];

    [JsonPropertyName("fully_diluted_valuation")]
    public Dictionary<string, decimal>? FullyDilutedValuation { get; init; }

    [JsonPropertyName("circulating_supply")]
    public decimal? CirculatingSupply { get; init; }

    [JsonPropertyName("total_supply")]
    public decimal? TotalSupply { get; init; }

    [JsonPropertyName("max_supply")]
    public decimal? MaxSupply { get; init; }
}

public class CoinImageData
{
    [JsonPropertyName("small")]
    public string? CoinIcon { get; init; }
}

public class MarketData
{
    [JsonPropertyName("current_price")] 
    public Dictionary<string, decimal>? CurrentPrice { get; init; }
    [JsonPropertyName("price_change_percentage_24h_in_currency")] 
    public Dictionary<string, decimal>? PriceChange24h { get; init; }

    [JsonPropertyName("low_24h")] 
    public Dictionary<string, decimal>? Low24h { get; init; }
    [JsonPropertyName("high_24h")] 
    public Dictionary<string, decimal>? High24h { get; init; }

    [JsonPropertyName("market_cap")] 
    public Dictionary<string, decimal>? MarketCap { get; init; }
    [JsonPropertyName("fully_diluted_valuation")] 
    public Dictionary<string, decimal>? FullyDilutedValuation { get; init; }
    [JsonPropertyName("total_volume")] 
    public Dictionary<string, decimal>? TotalVolume { get; init; }

    [JsonPropertyName("total_supply")] 
    public decimal? TotalSupply { get; init; }
    [JsonPropertyName("max_supply")] 
    public decimal? MaxSupply { get; init; }

    [JsonPropertyName("circulating_supply")]
    public decimal? CirculatingSupply { get; init; }
}


public class CoinLinks
{
    [JsonPropertyName("homepage")]
    public string[] Homepage { get; init; } = [];
}

public class CoinMarketRow
{
    [JsonPropertyName("market")]
    public ExchangeInfo? Market { get; init; }

    [JsonPropertyName("base")]
    public string? Base { get; init; }

    [JsonPropertyName("target")]
    public string? Target { get; init; }

    [JsonPropertyName("last")]
    public decimal? Price { get; init; }

    [JsonPropertyName("volume")]
    public decimal? Volume24h { get; init; }

    [JsonPropertyName("trade_url")]
    public string? TradeUrl { get; init; }
}

public class ExchangeInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}