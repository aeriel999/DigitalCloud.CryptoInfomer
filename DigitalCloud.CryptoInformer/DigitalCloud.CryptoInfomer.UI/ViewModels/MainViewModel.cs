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

    public MainViewModel(ICoinGeckoClient coinGeckoClient)
    {
        _coinGeckoClient = coinGeckoClient;

        LoadCurrenciesCommand = new AsyncRelayCommand(LoadCurrenciesAsync);

        //_ = LoadCurrenciesAsync();
    }

    public IAsyncRelayCommand LoadCurrenciesCommand { get; }

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
}

