using System.Text.Json.Serialization;

namespace DigitalCloud.CryptoInformer.Application.Models.Response.Charts;

public class DataForListChartResponse
{
    [JsonPropertyName("prices")]
    public double[][] Prices { get; set; } = Array.Empty<double[]>();

    [JsonPropertyName("market_caps")]
    public double[][] MarketCaps { get; set; } = Array.Empty<double[]>();

    [JsonPropertyName("total_volumes")]
    public double[][] TotalVolumes { get; set; } = Array.Empty<double[]>();
}
