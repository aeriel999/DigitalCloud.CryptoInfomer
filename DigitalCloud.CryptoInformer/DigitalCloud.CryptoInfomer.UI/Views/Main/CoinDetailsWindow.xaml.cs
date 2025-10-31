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

        public CoinDetailsWindow(string coinId)
        {
            InitializeComponent();

            _coinId = coinId;
        }
    }
}
