using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using System.Diagnostics;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels;

public partial class CoinDetailsViewModel : ObservableObject
{
    private readonly ICoinGeckoClient _coinGeckoClient;

    [ObservableProperty]
    private GetCoinDetailsResponse? _coinDetails;

    [ObservableProperty]
    private GetCoinDetailsResponse? _coin;

    [ObservableProperty]
    private bool _isLoading;

    public CoinDetailsViewModel(ICoinGeckoClient coinGeckoClient)
    {
        _coinGeckoClient = coinGeckoClient;

        OpenTradeCommand = new RelayCommand<string?>(OpenTrade);
    }

    public IRelayCommand<string?> OpenTradeCommand { get; }

    public async Task InitializeAsync(string coinId)
    {
        try
        {
            IsLoading = true;

            var coinDetailsRequest = new GetCoinDetailsRequest(
                                                    CoinId: coinId,
                                                    IncludeTickers: true,
                                                    IncludeLocalization: false,
                                                    IncludeMarketData: true,
                                                    IncludeCommunityData: false,
                                                    IncludeDeveloperData: false,
                                                    IncludeSparkline: false);

            var result = await _coinGeckoClient.GetDetailsInformationForCoinAsync(coinDetailsRequest);

            if (result.IsError)
            {

                return;
            }

            Coin = result.Value;
        }
        finally { IsLoading = false; }
    }

    private void OpenTrade(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return;

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        catch
        {
            // error
        }
    }
}
