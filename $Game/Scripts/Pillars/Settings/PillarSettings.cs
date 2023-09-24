using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public record PillarSettings (
    [property: JsonProperty] int PillarId,
    [property: JsonProperty] int PillarScoreRequirement,
    [property: JsonProperty] float PillarSpeed,
    [property: JsonProperty] double PillarSpawnInterval
)
: IPillarSettings;