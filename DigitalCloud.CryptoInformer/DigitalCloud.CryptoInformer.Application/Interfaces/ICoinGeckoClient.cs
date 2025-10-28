using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using System.Collections.ObjectModel;

namespace DigitalCloud.CryptoInformer.Application.Interfaces;

public interface ICoinGeckoClient
{
    public Task<List<CurrencyInfoResponse>> 
        GetListOfCurrenciesAsync(GetCurrenciesListRequest reuest);

    public Task<List<CurrencyInfoResponse>> GetListOfCurrenciesAsync();
}
