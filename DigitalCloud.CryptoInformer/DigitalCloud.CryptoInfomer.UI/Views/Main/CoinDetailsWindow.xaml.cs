using DigitalCloud.CryptoInfomer.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalCloud.CryptoInfomer.UI.Views.Main
{
    /// <summary>
    /// Interaction logic for CoinDetailsWindow.xaml
    /// </summary>
    public partial class CoinDetailsWindow : Window
    {
        private readonly string _coinId;

        private readonly CoinDetailsViewModel _coinDetailsViewModel;


        public CoinDetailsWindow(string coinId)
        {
            InitializeComponent();

            _coinId = coinId;

            _coinDetailsViewModel = App.Current.Services.GetService<CoinDetailsViewModel>()!;
            DataContext = _coinDetailsViewModel;

            Loaded += OnWindowLoaded;
        }

        private async void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            await _coinDetailsViewModel.InitializeAsync(_coinId);
        }
    }
}
