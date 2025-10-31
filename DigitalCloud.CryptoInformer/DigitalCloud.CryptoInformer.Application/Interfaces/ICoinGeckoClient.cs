using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using ErrorOr;

namespace DigitalCloud.CryptoInformer.Application.Interfaces;

public interface ICoinGeckoClient
{
    public Task<ErrorOr<List<CurrencyInfoResponse>>>GetListOfCurrenciesAsync(GetCurrenciesListRequest reuest);

    public Task<ErrorOr<List<CurrencyInfoResponse>>> GetListOfCurrenciesAsync();

    public Task<ErrorOr<GetCoinDetailsResponse>> GetDetailsInformationForCoinAsync(GetCoinDetailsRequest request);
}
