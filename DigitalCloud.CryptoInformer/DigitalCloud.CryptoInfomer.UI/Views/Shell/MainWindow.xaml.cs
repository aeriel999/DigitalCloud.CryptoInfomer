using DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces;
using DigitalCloud.CryptoInfomer.UI.ViewModels;
using DigitalCloud.CryptoInfomer.UI.Views.Pages;
using System.Windows;

namespace DigitalCloud.CryptoInfomer.UI.Views.Shell;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(ShellViewModel viewModel, IDigitalCloudNavigationService navigation)
    {
        InitializeComponent();

        DataContext = viewModel;

        navigation.Initialize(MainFrame);

        navigation.NavigateTo<CoinsListPage>();
    }
}

 