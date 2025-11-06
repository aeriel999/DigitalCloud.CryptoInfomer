using System.Globalization;
using System.Resources;


namespace DigitalCloud.CryptoInformer.Infrastructure.Helpers;


public static class LocalizationHelper
{
    private static readonly ResourceManager _resources =
           new ResourceManager("DigitalCloud.CryptoInformer.Infrastructure.Resources.ErrorMessages",
               typeof(LocalizationHelper).Assembly);

    public static string Get(string key, string locale = "en")
    {
        var culture = new CultureInfo(locale);
        return _resources.GetString(key, culture) ?? key;
    }
}
