using DigitalCloud.CryptoInformer.Application.Models.Requests.Charts;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Currency;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Dropdown;
using DigitalCloud.CryptoInformer.Application.Models.Response.Charts;
using DigitalCloud.CryptoInformer.Application.Models.Response.Currency;
using DigitalCloud.CryptoInformer.Application.Models.Response.Dropdowns;
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

    public Task<ErrorOr<DataForListChartResponse>> GetDataForListChart(GetDataForListChartRequest request);

    public Task<ErrorOr<IReadOnlyList<DataForCandlestickChartResponse>>> GetDataForCandlestickChart(
        GetDataForCandlestickChartRequest request);
}
