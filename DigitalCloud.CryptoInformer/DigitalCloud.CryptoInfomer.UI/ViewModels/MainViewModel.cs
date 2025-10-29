using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using System.Collections.ObjectModel;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ICoinGeckoClient _coinGeckoClient;

    [ObservableProperty]
    private ObservableCollection<CurrencyInfoResponse> _currencies = new();

    [ObservableProperty]
    private bool _isLoading;

    public MainViewModel(ICoinGeckoClient coinGeckoClient)
    {
        _coinGeckoClient = coinGeckoClient;

        LoadCurrenciesCommand = new AsyncRelayCommand(LoadCurrenciesAsync);
    }

    public IAsyncRelayCommand LoadCurrenciesCommand { get; }

    private async Task LoadCurrenciesAsync()
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

    //private TaskNotifier<ErrorOr<List<CurrencyInfoResponse>>> currencyInfoResponses;

    //public Task<ErrorOr<List<CurrencyInfoResponse>>>? CurrencyInfoResponses
    //{
    //    get => currencyInfoResponses;
    //    set => SetPropertyAndNotifyOnCompletion(ref currencyInfoResponses, value);
    //}

    //public void RequestValue()
    //{
    //    CurrencyInfoResponses = coinGeckoClient.GetListOfCurrenciesAsync();

    //    var res = CurrencyInfoResponses.Result
    //}
}

