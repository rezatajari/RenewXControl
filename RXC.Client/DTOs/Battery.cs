public record Battery(
    string AssetType,
    double Capacity,
    double? StateCharge,
    double? RateDischarge,
    string Message,
    double SetPoint,
    DateTime Timestamp)
    : Asset(AssetType, Message, SetPoint, Timestamp);
