using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public record LevelChangeUniqueSettings(
    [property: JsonProperty] int Id, 
    [property: JsonProperty] int PillarsPassedRequirement
) : ILevelChangeUniqueSettings;