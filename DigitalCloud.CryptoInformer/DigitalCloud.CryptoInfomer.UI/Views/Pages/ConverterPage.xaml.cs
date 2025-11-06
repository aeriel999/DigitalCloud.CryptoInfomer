using DigitalCloud.CryptoInfomer.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DigitalCloud.CryptoInfomer.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for ConverterPage.xaml
    /// </summary>
    public partial class ConverterPage : Page
    {
        public ConverterPage(ConverterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void PricePlot_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ConverterViewModel vm
                && vm.FromCurrencyCurrentCoin is not null
                && vm.PriceLinePlotModel is null)
            {
                _ = vm.LoadPriceChartAsync();   
            }
        }

        private void PricePlot_Unloaded(object sender, RoutedEventArgs e)
        {
            var pv = (OxyPlot.Wpf.PlotView)sender;
            pv.Model = null;
            if (DataContext is ConverterViewModel vm)
                vm.PriceLinePlotModel = null;
        }
    }
}
