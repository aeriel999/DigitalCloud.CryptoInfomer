using DigitalCloud.CryptoInfomer.UI.Services.Navigation;
using DigitalCloud.CryptoInfomer.UI.ViewModels;
using DigitalCloud.CryptoInfomer.UI.Views.Pages;
using DigitalCloud.CryptoInfomer.UI.Views.Shell;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalCloud.CryptoInfomer.UI;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<ShellViewModel>();
        services.AddSingleton<CoinDetailsViewModel>();
        services.AddSingleton<CoinsListViewModel>();
        services.AddSingleton<ConverterViewModel>();

        services.AddSingleton<MainWindow>();
        services.AddTransient<CoinsListPage>();
        services.AddTransient<CoinDetailsPage>();
        services.AddTransient<ConverterPage>();

        services.AddSingleton<IDigitalCloudNavigationService, DigitalCloudNavigationService>();

        return services;
    }
}