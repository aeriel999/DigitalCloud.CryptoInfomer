using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace DigitalCloud.CryptoInfomer.UI.Views.Shared
{
    /// <summary>
    /// Interaction logic for AppHeader.xaml
    /// </summary>
    public partial class AppHeader : UserControl
    {
        public AppHeader()
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
#pragma warning disable WPF0001
                var isDark = Application.Current.ThemeMode == ThemeMode.Dark;
#pragma warning restore WPF0001

                ThemeSwitch.IsChecked = isDark;
                ApplyTheme(isDark);  
            };
        }


        private void ThemeSwitch_Click(object sender, RoutedEventArgs e)
        {
            var isDark = ThemeSwitch.IsChecked == true;
            ApplyTheme(isDark);
        }


        private static void ApplyTheme(bool isDark)
        {
#pragma warning disable WPF0001
            Application.Current.ThemeMode = isDark ? ThemeMode.Dark : ThemeMode.Light;
#pragma warning restore WPF0001

            var key = isDark ? "AppLogoDark" : "AppLogoLight";

            Application.Current.Resources["AppLogo"] =
                (ImageSource)Application.Current.Resources[key];
        }
    }
}