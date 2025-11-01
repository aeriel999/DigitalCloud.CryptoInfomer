using Microsoft.Extensions.DependencyInjection;
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
                throw new InvalidOperationException("DigitalCloudNavigationService is not initialized with Frame.");

            var page = _serviceProvider.GetRequiredService<TPage>();
            _frame.Navigate(page);
        }
    }
}
