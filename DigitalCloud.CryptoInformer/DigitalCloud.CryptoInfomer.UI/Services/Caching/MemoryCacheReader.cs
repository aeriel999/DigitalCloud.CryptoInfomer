using Microsoft.Extensions.Caching.Memory;

namespace DigitalCloud.CryptoInfomer.UI.Services.Caching;

public class MemoryCacheReader(IMemoryCache cache) : ICacheReader
{
    public IEnumerable<object?> GetByKeys(IEnumerable<string> keys)
    {
        foreach (var k in keys)
            yield return cache.TryGetValue(k, out var v) ? v : null;
    }


    public HashSet<string> GetIndex(string indexKey)
    {
        return cache.TryGetValue(indexKey, out HashSet<string>? set) && set is not null
            ? set
            : new HashSet<string>();
    }
}
