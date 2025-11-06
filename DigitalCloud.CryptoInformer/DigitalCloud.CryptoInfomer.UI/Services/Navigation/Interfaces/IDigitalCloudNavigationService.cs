using System.Windows.Controls;

namespace DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces
{
    public interface IDigitalCloudNavigationService
    {
        public void Initialize(Frame frame);

        public void NavigateTo<TPage>() where TPage : Page;

        public void NavigateTo<TPage, TParam>(TParam parameter) where TPage : Page;
    }
}
