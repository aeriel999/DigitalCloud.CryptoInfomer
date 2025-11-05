using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using ErrorOr;

namespace DigitalCloud.CryptoInformer.Application.Interfaces;

public interface ICoinGeckoClient
{
    public Task<ErrorOr<List<GetCurrenciesListResponse>>>GetListOfCurrenciesAsync(
        GetCurrenciesListRequest reuest);

    public Task<ErrorOr<List<GetCurrenciesListResponse>>> GetListOfCurrenciesAsync();

    public Task<ErrorOr<GetCoinDetailsResponse>> GetDetailsInformationForCoinAsync(
        GetCoinDetailsRequest request);

    public Task<ErrorOr<List<GetCoinsListForDropdawnResponse>>> GetCoinsListForDropdawnAsync(
        GetCoinsListForDropdawnRequest request);

    public Task<ErrorOr<MarketChartResponse>> GetDataForMarketChart(GetMarketChartByIdRequest request);
}
