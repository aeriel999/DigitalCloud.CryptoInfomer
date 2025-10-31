using DigitalCloud.CryptoInfomer.UI.Views.Main;
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

        private void HomeMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();

            mainWindow.Show();

            Window.GetWindow(this)?.Close();
        }
    }


}
