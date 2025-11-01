using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInfomer.UI.Views.Main;
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
    private ObservableCollection<GetCurrenciesListResponse> _currencies = new();

    [ObservableProperty]
    private bool _isMoreBtnVisible;

    [ObservableProperty]
    private bool _isLoading;

    private const int ITEM_PER_PAGE = 10;

    private int? _amountOfPage;
    private int _numberOfPage;

    public MainViewModel(ICoinGeckoClient coinGeckoClient)
    {
        _coinGeckoClient = coinGeckoClient;

        IsMoreBtnVisible = false;

         _numberOfPage = 1;

        _amountOfPage = 1;

        InitialLoadCurrenciesCommand = new AsyncRelayCommand(InitialLoadCurrenciesAsync);

       _ = InitialLoadCurrenciesAsync();

        SetTop10ModeCommand = new AsyncRelayCommand(SetTop10ModeAsync);
        SetTop100ModeCommand = new AsyncRelayCommand(SetTop100ModeAsync);
        SetAllListModeCommand = new AsyncRelayCommand(SetAllListModeAsync);
        LoadNextPartForCurenciesListCommand = new AsyncRelayCommand(LoadNextPartForCurenciesListAsync);
    }

    public IAsyncRelayCommand InitialLoadCurrenciesCommand { get; }
    public IAsyncRelayCommand SetTop10ModeCommand { get; }
    public IAsyncRelayCommand SetTop100ModeCommand { get; }
    public IAsyncRelayCommand SetAllListModeCommand { get; }
    public IAsyncRelayCommand LoadNextPartForCurenciesListCommand { get; }

    [RelayCommand]
    private void OpenCoinDetails(string coinId)
    {
        if (string.IsNullOrWhiteSpace(coinId))
            return;

        var detailsWindow = new CoinDetailsWindow(coinId);

        detailsWindow.Show();
    }

    private async Task InitialLoadCurrenciesAsync()
    {
        try
        {
            IsLoading = true;

            var result = await _coinGeckoClient.GetListOfCurrenciesAsync();
            if (result.IsError)
            {

                return;
            }

            foreach (var item in result.Value)
                Currencies.Add(item);
        }
        finally { IsLoading = false; }
    }

    private async Task SetTop10ModeAsync()
    {
        _numberOfPage = 1;
        _amountOfPage = 1;
        IsMoreBtnVisible = false;

        await LoadFirstTop10CurrenciesAsync();
    }

    private async Task SetTop100ModeAsync()
    {
        _numberOfPage = 1;
        _amountOfPage = 10;
        IsMoreBtnVisible = true;

        await LoadFirstTop10CurrenciesAsync();
    }

    private async Task SetAllListModeAsync()
    {
        _numberOfPage = 1;
        _amountOfPage = null;
        IsMoreBtnVisible = true;

        await LoadFirstTop10CurrenciesAsync();
    }

    private async Task LoadNextPartForCurenciesListAsync()
    {
        _numberOfPage++;

        await LoadFirstTop10CurrenciesAsync();
    }

    private async Task LoadFirstTop10CurrenciesAsync()
    {
        try
        {
            IsLoading = true;

        //Logic for first page load (Top10/Top100/All)
        if (_numberOfPage == 1)
            Currencies.Clear();
        // Hide "More" button after loading all Top100 pages
        else if (_amountOfPage != null && _numberOfPage == _amountOfPage)
            IsMoreBtnVisible = false;
       
            var _currentRequest = new GetCurrenciesListRequest(
                                     ItemsPerPage: ITEM_PER_PAGE,
                                     NumberOfPage: _numberOfPage,
                                     CurrencyListOrder: MarketCurrenciesOrder.MARKET_CAP_DESC,
                                     Currency: MarketCurrencies.USD,
                                     Locale: ApiLocale.EN,
                                     CurrenciesPricePresision: CurrencyPricePrecision.P2,
                                     TimeFrame: TimeFramesForPriceChangePercentage.H24, 
                                     IncludeSparkline: false);

        var result = await _coinGeckoClient.GetListOfCurrenciesAsync(_currentRequest);

        if (result.IsError)
            return;

         // Hide "More" button after loading all Top100 pages
         if (_amountOfPage == null && result.Value.Count < ITEM_PER_PAGE)
                 IsMoreBtnVisible = false;

            foreach (var item in result.Value)
            Currencies.Add(item);
        }
        finally { IsLoading = false; }
    }
}

