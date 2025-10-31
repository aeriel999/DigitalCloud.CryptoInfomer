using DigitalCloud.CryptoInfomer.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalCloud.CryptoInfomer.UI.Views.Main;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Style _defaultStyle;
    private readonly Style _accentStyle;

    public MainWindow()
    {
        InitializeComponent();

        _defaultStyle = (Style)Application.Current.Resources["DefaultButtonStyle"];
        _accentStyle = (Style)Application.Current.Resources["AccentButtonStyle"];

        Top_10_Button.Style = _accentStyle;

        MoreBtn.Visibility = Visibility.Hidden;

        DataContext = App.Current.Services.GetService<MainViewModel>();
    }

    private void ThemeSwitch_Click(object sender, RoutedEventArgs e)
    {
        bool isDark = ThemeSwitch.IsChecked == true;
        // switch the theme
    }

    private void Top_10_Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;

        button.Style = _accentStyle;

        Top_100_Button.Style = _defaultStyle;
        All_Button.Style = _defaultStyle;

        MoreBtn.Visibility = Visibility.Hidden;
    }

    private void Top_100_Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;

        button.Style = _accentStyle;

        Top_10_Button.Style = _defaultStyle;
        All_Button.Style = _defaultStyle;

        MoreBtn.Visibility = Visibility.Visible;
    }

    private void All_Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;

        button.Style = _accentStyle;

        Top_100_Button.Style = _defaultStyle;
        Top_10_Button.Style = _defaultStyle;

        MoreBtn.Visibility = Visibility.Visible;
    }
}