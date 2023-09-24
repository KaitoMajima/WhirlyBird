using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public record PillarSettings (
    [property: JsonProperty] int PillarPassRequirement,
    [property: JsonProperty] float PillarSpeed,
    [property: JsonProperty] double PillarSpawnInterval
)
: IPillarSettings;