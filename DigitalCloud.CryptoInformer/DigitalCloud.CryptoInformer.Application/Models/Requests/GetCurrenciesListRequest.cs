namespace DigitalCloud.CryptoInformer.Application.Models.Requests;

public record GetCurrenciesListRequest(
    int ItemsPerPage,
    int NumberOfPage,
    string CurrencyListOrder,
    string Currency,
    string Locale,
    string CurrenciesPricePresision,
    string TimeFrame);

