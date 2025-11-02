using System.Windows.Controls;

namespace DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces
{
    public interface IDigitalCloudNavigationService
    {
        void Initialize(Frame frame);

        void NavigateTo<TPage>() where TPage : Page;

        void NavigateTo<TPage, TParam>(TParam parameter) where TPage : Page;
    }
}
