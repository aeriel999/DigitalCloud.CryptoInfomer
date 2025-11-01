using System.Windows.Controls;

namespace DigitalCloud.CryptoInfomer.UI.Services.Navigation
{

    public interface IDigitalCloudNavigationService
    {
        void Initialize(Frame frame);

        void NavigateTo<TPage>() where TPage : Page;
    }
}
