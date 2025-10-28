using System.Runtime.Serialization;

namespace DigitalCloud.CryptoInformer.Application.Helpers.Enums;

public enum TimeFramesForPriceChangePercentage
{
    [EnumMember(Value = "1h")]
    H1,

    [EnumMember(Value = "24h")]
    H24,

    [EnumMember(Value = "7d")]
    D7,

    [EnumMember(Value = "14d")]
    D14,

    [EnumMember(Value = "30d")]
    D30,

    [EnumMember(Value = "200d")]
    D200,

    [EnumMember(Value = "1y")]
    Y1
}
