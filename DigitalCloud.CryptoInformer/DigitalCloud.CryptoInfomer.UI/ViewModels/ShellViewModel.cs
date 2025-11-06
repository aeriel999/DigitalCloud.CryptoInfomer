using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces;
using DigitalCloud.CryptoInfomer.UI.Views.Pages;


namespace DigitalCloud.CryptoInfomer.UI.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
       private readonly IDigitalCloudNavigationService _navigationService;


       public enum AppPage { CoinsList, Converter, CoinDetails }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GoToCoinsListCommand))]
        [NotifyCanExecuteChangedFor(nameof(GoToConverterCommand))]
        [NotifyCanExecuteChangedFor(nameof(GoToCoinDetailsCommand))]
        private AppPage _currentPage;

        private bool CanGoToCoinsList() => CurrentPage != AppPage.CoinsList;
        private bool CanGoToConverter() => CurrentPage != AppPage.Converter;
        private bool CanGoToCoinDetails() => CurrentPage != AppPage.CoinDetails;


        public ShellViewModel(IDigitalCloudNavigationService navigationService)
        {
            _navigationService = navigationService;

            _navigationService.Navigated += OnNavigated; // added

            CurrentPage = AppPage.CoinsList;
        }


        private void OnNavigated(Type pageType)
        {
            CurrentPage =
                pageType == typeof(CoinsListPage) ? AppPage.CoinsList :
                pageType == typeof(ConverterPage) ? AppPage.Converter :
                pageType == typeof(CoinDetailsPage) ? AppPage.CoinDetails :
                CurrentPage;
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
