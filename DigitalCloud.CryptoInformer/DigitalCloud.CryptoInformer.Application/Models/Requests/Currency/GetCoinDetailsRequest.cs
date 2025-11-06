namespace DigitalCloud.CryptoInformer.Application.Models.Requests.Currency;

public record GetCoinDetailsRequest(
    string CoinId,
    bool IncludeTickers,
    bool IncludeLocalization,
    bool IncludeMarketData,
    bool IncludeCommunityData,
    bool IncludeDeveloperData,
    bool IncludeSparkline);
 
