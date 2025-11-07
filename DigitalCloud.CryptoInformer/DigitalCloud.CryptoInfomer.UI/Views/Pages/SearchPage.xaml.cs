using DigitalCloud.CryptoInfomer.UI.ViewModels;
using System.Windows.Controls;


namespace DigitalCloud.CryptoInfomer.UI.Views.Pages;
/// <summary>
/// Interaction logic for SearchPage.xaml
/// </summary>
public partial class SearchPage : Page
{
    public SearchPage(SearchViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
