using System.Windows.Controls;

namespace DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces
{
    public interface IDigitalCloudNavigationService
    {
        event Action<Type>? Navigated;

        public void Initialize(Frame frame);


        public void NavigateTo<TPage>() where TPage : Page;


        public void NavigateTo<TPage, TParam>(TParam parameter) where TPage : Page;
    }
}
