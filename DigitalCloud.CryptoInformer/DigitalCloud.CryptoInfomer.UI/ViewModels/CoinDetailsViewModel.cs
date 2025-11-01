using CommunityToolkit.Mvvm.ComponentModel;
using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;

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
    }

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
}
