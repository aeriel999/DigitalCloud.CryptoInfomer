using DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces;
using DigitalCloud.CryptoInfomer.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DigitalCloud.CryptoInfomer.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for CoinDetailPage.xaml
    /// </summary>
    public partial class CoinDetailsPage : Page, INavigatable<string>
    {
        private readonly CoinDetailsViewModel _coinDetailsViewModel;


        public CoinDetailsPage(CoinDetailsViewModel viewModel)
        {
            InitializeComponent();

            _coinDetailsViewModel = viewModel;
            DataContext = _coinDetailsViewModel;
        }


        public async void OnNavigatedTo(string coinId)
        {
            if (string.IsNullOrWhiteSpace(coinId))
                return;

            await _coinDetailsViewModel.InitializeAsync(coinId);
        }


        private void LineChart_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CoinDetailsViewModel vm)
            {
                if (vm.Coin is not null && (vm.PriceModel is null || vm.PriceModel.Series.Count == 0))
                    vm.BuildChart();
            }
        }


        private void LineChart_Unloaded(object sender, RoutedEventArgs e)
        {
            var pv = (OxyPlot.Wpf.PlotView)sender;
            pv.Model = null;
        }

    }
}
