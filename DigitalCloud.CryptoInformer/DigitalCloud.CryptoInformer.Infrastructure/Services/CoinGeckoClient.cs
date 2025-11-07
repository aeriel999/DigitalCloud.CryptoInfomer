using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Charts;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Currency;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Dropdown;
using DigitalCloud.CryptoInformer.Application.Models.Requests.Search;
using DigitalCloud.CryptoInformer.Application.Models.Response.Charts;
using DigitalCloud.CryptoInformer.Application.Models.Response.Currency;
using DigitalCloud.CryptoInformer.Application.Models.Response.Dropdowns;
using DigitalCloud.CryptoInformer.Application.Models.Response.Search;
using DigitalCloud.CryptoInformer.Application.Utils;
using DigitalCloud.CryptoInformer.Infrastructure.Helpers;
using ErrorOr;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using System.Text.Json;

namespace DigitalCloud.CryptoInformer.Infrastructure.Services;

internal class CoinGeckoClient(HttpClient httpClient, string url, IMemoryCache cache) : ICoinGeckoClient
{
    public async Task<ErrorOr<List<GetCurrenciesListResponse>>>GetListOfCurrenciesAsync(
		GetCurrenciesListRequest request)
    {
        var cacheKey =
            $"{request.Currency}:{request.CurrencyListOrder}:{request.ItemsPerPage}:{request.NumberOfPage}:" +
            $"{request.IncludeSparkline}:{request.TimeFrame}:{request.Locale}:{request.CurrenciesPricePresision}";

        if (cache.TryGetValue(cacheKey, out List<GetCurrenciesListResponse>? cachedPage))
            return cachedPage!;

        try
		{
			var result = await httpClient.GetFromJsonAsync<List<GetCurrenciesListResponse>>(
				$"{url}/coins/markets?" +
				$"vs_currency={request.Currency}" +
				$"&order={request.CurrencyListOrder}" +
				$"&per_page={request.ItemsPerPage}" +
				$"&page={request.NumberOfPage}" +
                $"&sparkline={request.IncludeSparkline.ToString().ToLowerInvariant()}" +
                $"&price_change_percentage={request.TimeFrame}" +
				$"&locale={request.Locale}" +
				$"&precision={request.CurrenciesPricePresision}");

			if (result == null)
				return Error.NotFound(
						  code: "Empty.Result",
						  description: LocalizationHelper.Get("EmptyResult"));

            cache.Set(cacheKey, result, TimeSpan.FromHours(24));

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
        const string cacheKey = "coins.default";

        if (cache.TryGetValue(cacheKey, out List<GetCurrenciesListResponse>? cachedList))
            return cachedList!;

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

            cache.Set(cacheKey, result, TimeSpan.FromHours(24));

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
                $"?localization={request.IncludeLocalization.ToString().ToLowerInvariant()}" +
                $"&tickers={request.IncludeTickers.ToString().ToLowerInvariant()}" +
                $"&market_data={request.IncludeMarketData.ToString().ToLowerInvariant()}"+
                $"&community_data={request.IncludeCommunityData.ToString().ToLowerInvariant()}"+
                $"&developer_data={request.IncludeDeveloperData.ToString().ToLowerInvariant()}"+
                $"&sparkline={request.IncludeSparkline.ToString().ToLowerInvariant()}");

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

    public async Task<ErrorOr<List<GetCoinsListForDropdawnResponse>>> GetCoinsListForDropdawnAsync(
        GetCoinsListForDropdawnRequest request)
    {
        var cacheKey = $"coins.list_{request.NumderOfPage}";

        if (cache.TryGetValue(cacheKey, out List<GetCoinsListForDropdawnResponse>? cachedList))
            return cachedList!;

        try
        {
            var result = await httpClient.GetFromJsonAsync<List<GetCoinsListForDropdawnResponse>>(
                $"{url}/coins/markets?" +
                "vs_currency=usd" +
                "&order=market_cap_desc" +
                $"&per_page={request.RecordsPerPage}" +
                $"&page={request.NumderOfPage}" +
                "&sparkline=false" +
                "&price_change_percentage=24h");

            if (result == null)
                return Error.NotFound(
                          code: "Empty.Result",
                          description: LocalizationHelper.Get("EmptyResult"));

            cache.Set(cacheKey, result, TimeSpan.FromHours(24));

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

    public async Task<ErrorOr<DataForListChartResponse>> GetDataForListChartAsync(GetDataForListChartRequest request)
    {
        //TODO CASH
        try
        {
            var result = await httpClient.GetFromJsonAsync<DataForListChartResponse>(
                $"{url}/coins/" +
                $"{request.CoinId}/market_chart?" +
                $"vs_currency={request.VsCurrency}" +
                $"&days={request.Days}" +
                $"&interval={request.MarketChartInterval}" +
                $"&precision={request.CurrencyPricePrecision}");


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

    public async Task<ErrorOr<IReadOnlyList<DataForCandlestickChartResponse>>> GetDataForCandlestickChartAsync(GetDataForCandlestickChartRequest request)
    {
        //TODO CASH
        try
        {
            var result = await httpClient.GetFromJsonAsync<List<List<decimal>>>(
                    $"{url}/coins/{request.CoinId}/ohlc?" +
                    $"vs_currency={request.VsCurrency}" +
                    $"&days={request.DaysPeriod}" +
                    $"&precision={request.CurrencyPricePrecision}");

            if (result == null)
                return Error.NotFound(
                          code: "Empty.Result",
                          description: LocalizationHelper.Get("EmptyResult"));

            IReadOnlyList<DataForCandlestickChartResponse> mapped = OhlcMapper.FromRaw(result);

            return ErrorOrFactory.From(mapped);  
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

    public async Task<ErrorOr<GetSearchCoinsResponse>> GetDataForSearchAsync(GetSearchCoinsRequest request)
    {
        try
        {
            var result = await httpClient.GetFromJsonAsync<GetSearchCoinsResponse>(
                    $"{url}/search?query={request.Query}");

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