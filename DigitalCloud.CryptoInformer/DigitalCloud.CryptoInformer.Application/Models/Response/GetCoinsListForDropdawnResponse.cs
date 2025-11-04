using System.Text.Json.Serialization;

namespace DigitalCloud.CryptoInformer.Application.Models.Response;

public class GetCoinsListForDropdawnResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("symbol")]
    public required string Symbol { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
