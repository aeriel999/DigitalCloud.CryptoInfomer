using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Helpers.Constants;
using DigitalCloud.CryptoInformer.Application.Helpers.Enums;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ICoinGeckoClient _coinGeckoClient;

    [ObservableProperty]
    private ObservableCollection<CurrencyInfoResponse> _currencies = new();

    private const int ITEM_PER_PAGE = 10;

    private int? _amountOfPage;
    private int _numberOfPage;

    public MainViewModel(ICoinGeckoClient coinGeckoClient)
    {
        _coinGeckoClient = coinGeckoClient;

        _numberOfPage = 1;

        _amountOfPage = 1;

        InitialLoadCurrenciesCommand = new AsyncRelayCommand(InitialLoadCurrenciesAsync);

       // _ = InitialLoadCurrenciesAsync();

        SetTop10ModeCommand = new AsyncRelayCommand(SetTop10Mode);
        SetTop100ModeCommand = new AsyncRelayCommand(SetTop100Mode);
        SetAllListModeCommand = new AsyncRelayCommand(SetAllListMode);
    }

    public IAsyncRelayCommand InitialLoadCurrenciesCommand { get; }
    public IAsyncRelayCommand SetTop10ModeCommand { get; }
    public IAsyncRelayCommand SetTop100ModeCommand { get; }
    public IAsyncRelayCommand SetAllListModeCommand { get; }

    private async Task InitialLoadCurrenciesAsync()
    {
        var result = await _coinGeckoClient.GetListOfCurrenciesAsync();
        if (result.IsError)
        {

            return;
        }

        foreach (var item in result.Value)
            Currencies.Add(item);
    }

    private async Task SetTop10Mode()
    {
        _numberOfPage = 1;
        _amountOfPage = 1;

        await LoadFirstTop10CurrenciesAsync();
    }

    private async Task SetTop100Mode()
    {
        _numberOfPage = 1;
        _amountOfPage = 10;

        await LoadFirstTop10CurrenciesAsync();
    }

    private async Task SetAllListMode()
    {
        _numberOfPage = 1;
        _amountOfPage = null;

        await LoadFirstTop10CurrenciesAsync();
    }

    private async Task LoadFirstTop10CurrenciesAsync()
    {
        //Logic for first page load (Top10/Top100/All)
        if (_numberOfPage == 1)
            Currencies.Clear();
        
        var _currentRequest = new GetCurrenciesListRequest(
                                     ItemsPerPage: ITEM_PER_PAGE,
                                     NumberOfPage: _numberOfPage,
                                     CurrencyListOrder: MarketCurrenciesOrder.MARKET_CAP_DESC,
                                     Currency: MarketCurrencies.USD,
                                     Locale: ApiLocale.EN,
                                     CurrenciesPricePresision: CurrencyPricePrecision.P2,
                                     TimeFrame: TimeFramesForPriceChangePercentage.H24);

        var result = await _coinGeckoClient.GetListOfCurrenciesAsync(_currentRequest);

        if (result.IsError)
            return;

        foreach (var item in result.Value)
            Currencies.Add(item);
    }
}

