namespace DigitalCloud.CryptoInformer.Application.Models.Requests;

public record GetCoinDetailsRequest(
    string CoinId,
    bool IncludeTickers,
    bool IncludeLocalization,
    bool IncludeMarketData,
    bool IncludeCommunityData,
    bool IncludeDeveloperData,
    bool IncludeSparkline);
 
