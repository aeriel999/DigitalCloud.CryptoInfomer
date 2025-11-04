using DigitalCloud.CryptoInfomer.UI.ViewModels;
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
    }
}
