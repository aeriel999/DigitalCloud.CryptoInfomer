using System.Windows;
using System.Windows.Controls;

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
                ThemeSwitch.IsChecked = Application.Current.ThemeMode == ThemeMode.Dark;
            };
            ThemeSwitch.Click += ThemeSwitch_Click;
        }

        private void ThemeSwitch_Click(object? sender, RoutedEventArgs e)
        {
#pragma warning disable WPF0001
            var app = Application.Current;
            app.ThemeMode = (ThemeSwitch.IsChecked == true) ? ThemeMode.Dark : ThemeMode.Light;
#pragma warning restore WPF0001
        }
    }


}
