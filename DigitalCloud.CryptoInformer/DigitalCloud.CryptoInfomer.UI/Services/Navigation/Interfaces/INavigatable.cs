namespace DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces
{
    public interface INavigatable<in TParam>
    {
        void OnNavigatedTo(TParam parameter);
    }
}
