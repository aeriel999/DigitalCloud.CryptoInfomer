namespace DigitalCloud.CryptoInformer.Application.Models.Response;

public class GetCoinDetailsResponse
{
    // 1. Header
    public required string Id { get; init; }           // "bitcoin"
    public required string Name { get; init; }         // "Bitcoin"
    public required string Symbol { get; init; }       // "btc"
    public string? ImageUrl { get; init; }             // big icon
    public int? MarketCapRank { get; init; }

    // 2. Main market data (vs = usd)
    public decimal? CurrentPrice { get; init; }
    public decimal? PriceChange24hPercent { get; init; }
    public decimal? MarketCap { get; init; }
    public decimal? TotalVolume24h { get; init; }
    public decimal? High24h { get; init; }
    public decimal? Low24h { get; init; }

    // 3. Extra info
    public string? HashingAlgorithm { get; init; }
    public string? GenesisDate { get; init; }
    public string[] Categories { get; init; } = [];

    // 4. Links
    public string? HomepageUrl { get; init; }

    // 5. Markets table
    public List<CoinMarketRow> Markets { get; init; } = [];
}

public class CoinMarketRow
{
    public required string ExchangeName { get; init; }   // Binance
    public required string Pair { get; init; }           // BTC/USDT
    public decimal? Price { get; init; }                 // last
    public decimal? Volume24h { get; init; }
    public string? TradeUrl { get; init; }               // open in exchange
}