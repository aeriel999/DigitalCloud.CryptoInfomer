using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInfomer.UI.Services.Navigation;
using DigitalCloud.CryptoInfomer.UI.Views.Pages;
using System.Windows.Navigation;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
        private readonly IDigitalCloudNavigationService _navigationService;

        public ShellViewModel(IDigitalCloudNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private void GoToCoinsList() =>
            _navigationService.NavigateTo<CoinsListPage>();

        [RelayCommand]
        private void GoToConverter() =>
            _navigationService.NavigateTo<ConverterPage>();

        [RelayCommand]
        private void GoToCoinDetails() =>
            _navigationService.NavigateTo<CoinDetailsPage>();
    }
}
