using DigitalCloud.CryptoInformer.Application.Interfaces;
using DigitalCloud.CryptoInformer.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalCloud.CryptoInformer.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<ICoinGeckoClient>(sp =>
        {
            var http = sp.GetRequiredService<HttpClient>();

            http.BaseAddress = new Uri(config["CoinGeckoApiUrl"]!);
            http.DefaultRequestHeaders.UserAgent.ParseAdd("CryptoInformer/1.0");

            var apiKey = config["CoinGeckoApiKey"];
            if (!string.IsNullOrWhiteSpace(apiKey) &&
                !http.DefaultRequestHeaders.Contains("x-cg-demo-api-key"))
            {
                http.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);
            }

            return new CoinGeckoClient(http, config["CoinGeckoApiUrl"]!);
        });

        return services;
    }
}