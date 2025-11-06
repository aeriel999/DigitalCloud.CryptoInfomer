using DigitalCloud.CryptoInformer.Application.Models.Response.Charts;

namespace DigitalCloud.CryptoInformer.Application.Utils;

public static class OhlcMapper
{
    public static IReadOnlyList<DataForCandlestickChartResponse> FromRaw(List<List<decimal>> raw)
    {
        var list = new List<DataForCandlestickChartResponse>(raw.Count);

        foreach (var r in raw)
        {
            if (r is null || r.Count < 5) continue;
            list.Add(new DataForCandlestickChartResponse
            {
                TimestampMs = (long)r[0],
                Open = r[1],
                High = r[2],
                Low = r[3],
                Close = r[4]
            });
        }
        return list;
    }
}
