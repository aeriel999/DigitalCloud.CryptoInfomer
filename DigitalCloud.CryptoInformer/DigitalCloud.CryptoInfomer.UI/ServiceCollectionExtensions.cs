using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalCloud.CryptoInfomer.UI;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<ViewModel>();

        return services;
    }
}