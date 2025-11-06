namespace DigitalCloud.CryptoInformer.Application.Models.Requests.Charts;


public record GetDataForCandlestickChartRequest(
    string CoinId,
    string VsCurrency,
    string DaysPeriod,
    string? CurrencyPricePrecision);
 