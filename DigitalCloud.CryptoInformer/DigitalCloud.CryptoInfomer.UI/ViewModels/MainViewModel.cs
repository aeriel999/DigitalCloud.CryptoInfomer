using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Helpers.Constants;
using DigitalCloud.CryptoInformer.Application.Helpers.Enums;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using System.Collections.ObjectModel;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ICoinGeckoClient _coinGeckoClient;


    [ObservableProperty]
    private ObservableCollection<CurrencyInfoResponse> _currencies = new();


    private GetCurrenciesListRequest _currentRequest;

    public MainViewModel(ICoinGeckoClient coinGeckoClient)
    {
        _coinGeckoClient = coinGeckoClient;

        _currentRequest = new(
                                    ItemsPerPage: 11,
                                    NumberOfPage: 1,
                                    CurrencyListOrder: MarketCurrenciesOrder.MARKET_CAP_DESC,
                                    Currency: MarketCurrencies.USD,
                                    Locale: ApiLocale.EN,
                                    CurrenciesPricePresision: CurrencyPricePrecision.P2,
                                    TimeFrame: TimeFramesForPriceChangePercentage.H24);

        LoadCurrenciesCommand = new AsyncRelayCommand(LoadCurrenciesAsync);

        LoadFirstPartOfCurrenciesCommand = new AsyncRelayCommand(LoadFirstTop10CurrenciesAsync);

        //_ = LoadCurrenciesAsync();
    }

    public IAsyncRelayCommand LoadCurrenciesCommand { get; }
    public IAsyncRelayCommand LoadFirstPartOfCurrenciesCommand { get; }

    private async Task LoadCurrenciesAsync()
    {
        var result = await _coinGeckoClient.GetListOfCurrenciesAsync();
        if (result.IsError)
        {

            return;
        }

        foreach (var item in result.Value)
            Currencies.Add(item);
    }

    private async Task LoadFirstTop10CurrenciesAsync()
    {
        Currencies.Clear();

        //_currentRequest = _currentRequest with
        //{
        //    CurrencyListOrder = MarketCurrenciesOrder.MARKET_CAP_ASC
        //};

        var result = await _coinGeckoClient.GetListOfCurrenciesAsync(_currentRequest);
        if (result.IsError)
            return;

        foreach (var item in result.Value)
            Currencies.Add(item);
    }
}

