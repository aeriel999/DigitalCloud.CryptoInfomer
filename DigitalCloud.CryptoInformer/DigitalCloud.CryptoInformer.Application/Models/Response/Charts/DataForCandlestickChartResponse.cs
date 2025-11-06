namespace DigitalCloud.CryptoInformer.Application.Models.Response.Charts;

public class DataForCandlestickChartResponse
{
    public long TimestampMs { get; init; }
    public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);
    public decimal Open { get; init; }
    public decimal High { get; init; }
    public decimal Low { get; init; }
    public decimal Close { get; init; }
}
