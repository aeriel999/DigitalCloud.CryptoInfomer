using DigitalCloud.CryptoInformer.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalCloud.CryptoInformer.Infrastructure;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<ICoinGeckoClient>(sp =>
        {
            var http = sp.GetRequiredService<HttpClient>();

            return new CoinGeckoClient(http, config["CoinGeckoApiUrl"]!);
        });

        return services;
    }
}