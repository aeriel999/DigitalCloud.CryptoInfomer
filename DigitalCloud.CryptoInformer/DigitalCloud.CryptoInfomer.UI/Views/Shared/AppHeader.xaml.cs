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
        }

        private void ThemeSwitch_Click(object sender, RoutedEventArgs e)
        {
            bool isDark = ThemeSwitch.IsChecked == true;
            // switch the theme
        }

        //private void HomeMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    var mainWindow = new MainWindow();

        //    mainWindow.Show();

        //    Window.GetWindow(this)?.Close();
        //}
    }


}
