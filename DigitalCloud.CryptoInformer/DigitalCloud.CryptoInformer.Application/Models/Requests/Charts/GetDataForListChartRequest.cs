namespace DigitalCloud.CryptoInformer.Application.Models.Requests.Charts;

public record GetDataForListChartRequest(
    string CoinId,
    string VsCurrency,
    string Days,
    string? MarketChartInterval,
    string CurrencyPricePrecision);
 
