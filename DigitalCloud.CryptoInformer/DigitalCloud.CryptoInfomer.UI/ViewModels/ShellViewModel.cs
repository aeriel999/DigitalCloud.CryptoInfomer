using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces;
using DigitalCloud.CryptoInfomer.UI.Views.Pages;

namespace DigitalCloud.CryptoInfomer.UI.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
        private readonly IDigitalCloudNavigationService _navigationService;

        private bool CanGoToCoinsList() => CurrentPage != AppPage.CoinsList;
        private bool CanGoToConverter() => CurrentPage != AppPage.Converter;
        private bool CanGoToCoinDetails() => CurrentPage != AppPage.CoinDetails;


        [ObservableProperty]
        private AppPage _currentPage;

        public ShellViewModel(IDigitalCloudNavigationService navigationService)
        {
            _navigationService = navigationService;

            CurrentPage = AppPage.CoinsList;
        }


        public enum AppPage { CoinsList, Converter, CoinDetails }


        partial void OnCurrentPageChanged(AppPage value)
        {
            GoToCoinsListCommand.NotifyCanExecuteChanged();
            GoToConverterCommand.NotifyCanExecuteChanged();
            GoToCoinDetailsCommand.NotifyCanExecuteChanged();
        }


        [RelayCommand(CanExecute = nameof(CanGoToCoinsList))]
        private void GoToCoinsList()
        {
            if (CurrentPage == AppPage.CoinsList) return;
            _navigationService.NavigateTo<CoinsListPage>();
            CurrentPage = AppPage.CoinsList;
        }


        [RelayCommand(CanExecute = nameof(CanGoToConverter))]
        private void GoToConverter()
        {
            if (CurrentPage == AppPage.Converter) return;
            _navigationService.NavigateTo<ConverterPage>();
            CurrentPage = AppPage.Converter;
        }


        [RelayCommand(CanExecute = nameof(CanGoToCoinDetails))]
        private void GoToCoinDetails()
        {
            if (CurrentPage == AppPage.CoinDetails) return;
            _navigationService.NavigateTo<CoinDetailsPage>();
            CurrentPage = AppPage.CoinDetails;
        }
    }
}
