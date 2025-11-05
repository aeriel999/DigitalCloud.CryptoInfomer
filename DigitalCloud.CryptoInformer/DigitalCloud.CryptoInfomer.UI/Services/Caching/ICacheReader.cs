namespace DigitalCloud.CryptoInfomer.UI.Services.Caching;

public interface ICacheReader
{
    IEnumerable<object?> GetByKeys(IEnumerable<string> keys);

    HashSet<string> GetIndex(string indexKey);
}
