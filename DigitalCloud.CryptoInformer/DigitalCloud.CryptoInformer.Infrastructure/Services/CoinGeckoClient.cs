using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using DigitalCloud.CryptoInformer.Infrastructure.Helpers;
using ErrorOr;
using System.Net.Http.Json;
using System.Text.Json;

namespace DigitalCloud.CryptoInformer.Infrastructure.Services;

internal class CoinGeckoClient(HttpClient httpClient, string url) : ICoinGeckoClient
{
    public async Task<ErrorOr<List<GetCurrenciesListResponse>>>GetListOfCurrenciesAsync(
		GetCurrenciesListRequest request)
    {
		try
		{
			var result = await httpClient.GetFromJsonAsync<List<GetCurrenciesListResponse>>(
				$"{url}/coins/markets?" +
				$"vs_currency={request.Currency}" +
				$"&order={request.CurrencyListOrder}" +
				$"&per_page={request.ItemsPerPage}" +
				$"&page={request.NumberOfPage}" +
                $"&sparkline={request.IncludeSparkline}" +
                $"&price_change_percentage={request.TimeFrame}" +
				$"&locale={request.Locale}" +
				$"&precision={request.CurrenciesPricePresision}");

			if (result == null)
				return Error.NotFound(
						  code: "Empty.Result",
						  description: LocalizationHelper.Get("EmptyResult"));

            return result;
		}
		catch (HttpRequestException)
		{
            return Error.Failure(
				   code: "Network.Error",
                   description: LocalizationHelper.Get("NetworkError"));
        }
		catch (JsonException)
		{
            return Error.Failure(
					code: "Json.ParseError",
					description: LocalizationHelper.Get("JsonParseError"));
        }
		catch (Exception)
		{
            return Error.Failure(
					code: "General.Failure",
					description: LocalizationHelper.Get("GeneralFailure"));
        }
    }

    public async Task<ErrorOr<List<GetCurrenciesListResponse>>> GetListOfCurrenciesAsync()
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<List<GetCurrenciesListResponse>>(
                $"{url}/coins/markets?" +
                "vs_currency=usd" +
                "&order=market_cap_desc" +
                "&per_page=10" +
                "&page=1" +
                "&sparkline=false" +
                "&price_change_percentage=24h");

            if (result == null)
                return Error.NotFound(
                          code: "Empty.Result",
                          description: LocalizationHelper.Get("EmptyResult"));

            return result;
        }
        catch (HttpRequestException)
        {
            return Error.Failure(
                   code: "Network.Error",
                   description: LocalizationHelper.Get("NetworkError"));
        }
        catch (JsonException)
        {
            return Error.Failure(
                    code: "Json.ParseError",
                    description: LocalizationHelper.Get("JsonParseError"));
        }
        catch (Exception)
        {
            return Error.Failure(
                    code: "General.Failure",
                    description: LocalizationHelper.Get("GeneralFailure"));
        }
    }

    public async Task<ErrorOr<GetCoinDetailsResponse>> GetDetailsInformationForCoinAsync(
        GetCoinDetailsRequest request)
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<GetCoinDetailsResponse>(
                $"{url}/coins/" +
                $"{request.CoinId}" +
                $"?localization={request.IncludeLocalization}" +
                $" &tickers={request.IncludeTickers}" +
                $"&market_data={request.IncludeMarketData}"+
                $"&community_data={request.IncludeCommunityData}"+
                $"&developer_data={request.IncludeDeveloperData}"+
                $"&sparkline={request.IncludeSparkline}");

            if (result == null)
                return Error.NotFound(
                          code: "Empty.Result",
                          description: LocalizationHelper.Get("EmptyResult"));

            return result;
        }
        catch (HttpRequestException)
        {
            return Error.Failure(
                   code: "Network.Error",
                   description: LocalizationHelper.Get("NetworkError"));
        }
        catch (JsonException)
        {
            return Error.Failure(
                    code: "Json.ParseError",
                    description: LocalizationHelper.Get("JsonParseError"));
        }
        catch (Exception)
        {
            return Error.Failure(
                    code: "General.Failure",
                    description: LocalizationHelper.Get("GeneralFailure"));
        }
    }
}