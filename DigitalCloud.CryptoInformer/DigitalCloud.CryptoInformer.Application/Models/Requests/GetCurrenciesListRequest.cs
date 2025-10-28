using DigitalCloud.CryptoInformer.Application.Helpers.Enums;

namespace DigitalCloud.CryptoInformer.Application.Models.Requests;

public record GetCurrenciesListRequest(
    int ItemsPerPage,
    int NumberOfPage,
    MarketCurrenciesOrder CurrencyListOrder,
    MarketCurrencies Currency,
    ApiLocale Locale);

