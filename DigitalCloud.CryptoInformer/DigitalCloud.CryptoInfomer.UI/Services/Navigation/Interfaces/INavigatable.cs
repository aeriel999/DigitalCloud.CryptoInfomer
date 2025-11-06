namespace DigitalCloud.CryptoInfomer.UI.Services.Navigation.Interfaces
{
    public interface INavigatable<in TParam>
    {
        public void OnNavigatedTo(TParam parameter);
    }
}
