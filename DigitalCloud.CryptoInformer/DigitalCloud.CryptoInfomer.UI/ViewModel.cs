using CommunityToolkit.Mvvm.ComponentModel;
using DigitalCloud.CryptoInformer.Application.Interfaces;

namespace DigitalCloud.CryptoInfomer.UI;

public class ViewModel(ICoinGeckoClient coinGeckoClient) : ObservableObject
{
     
    public async Task InitializeAsync()
    {
        var result = await coinGeckoClient.GetListOfCurrenciesAsync();
    }
}

