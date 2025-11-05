namespace DigitalCloud.CryptoInformer.Application.Models.Requests;

public record GetMarketChartByIdRequest(
    string CoinId,
    string VsCurrency,
    string Days,
    string MarketChartInterval,
    string CurrencyPricePrecision);
 
