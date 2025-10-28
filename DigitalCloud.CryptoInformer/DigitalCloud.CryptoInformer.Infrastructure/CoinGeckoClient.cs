using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Application.Models.Requests;
using DigitalCloud.CryptoInformer.Application.Models.Response;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace DigitalCloud.CryptoInformer.Infrastructure;

internal class CoinGeckoClient(HttpClient httpClient, string url) : ICoinGeckoClient
{
    public async Task<List<CurrencyInfoResponse>> 
        GetListOfCurrenciesAsync(GetCurrenciesListRequest reuest)
    {
		try
		{
			var result = await httpClient.GetFromJsonAsync<List<CurrencyInfoResponse>>(
                $"{url}?" +
				$"vs_currency={reuest.Currency.ToString()}"+
				$"&order={reuest.CurrencyListOrder.ToString()}"+
				$"&per_page={reuest.ItemsPerPage}"+
				$"&page={reuest.NumberOfPage}"+
				$"&locale={reuest.Locale.ToString()}");

			if (result == null) throw new Exception("Empty list");

			return result;
        }
		catch (Exception)
		{

			throw;
		}
    }

    public Task<List<CurrencyInfoResponse>> GetListOfCurrenciesAsync()
    {
        throw new NotImplementedException();
    }
}