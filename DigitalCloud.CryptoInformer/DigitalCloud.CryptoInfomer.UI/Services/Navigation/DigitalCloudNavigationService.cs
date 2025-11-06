using DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace DigitalCloud.CryptoInfomer.UI.Services.Navigation
{
    public class DigitalCloudNavigationService : IDigitalCloudNavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private Frame? _frame;


        public DigitalCloudNavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public void Initialize(Frame frame)
        {
            _frame = frame;
        }


        public void NavigateTo<TPage>() where TPage : Page
        {
            if (_frame is null)
                throw new InvalidOperationException(
                    "DigitalCloudNavigationService is not initialized with Frame.");//TODO: add to localization

            var page = _serviceProvider.GetRequiredService<TPage>();
            _frame.Navigate(page);
        }


        public void NavigateTo<TPage, TParam>(TParam parameter) where TPage : Page
        {
            if (_frame is null)
                throw new InvalidOperationException(
                    "DigitalCloudNavigationService is not initialized with Frame.");// TODO: add to localization

            var page = _serviceProvider.GetRequiredService<TPage>();

            if (page is INavigatable<TParam> navPage)
                navPage.OnNavigatedTo(parameter);

            _frame.Navigate(page);
        }
    }
}
