using System.Text.Json.Serialization;

namespace DigitalCloud.CryptoInformer.Application.Models.Response;

public class MarketChartResponse
{
    [JsonPropertyName("prices")]
    public double[][] Prices { get; set; } = Array.Empty<double[]>();

    [JsonPropertyName("market_caps")]
    public double[][] MarketCaps { get; set; } = Array.Empty<double[]>();

    [JsonPropertyName("total_volumes")]
    public double[][] TotalVolumes { get; set; } = Array.Empty<double[]>();
}
