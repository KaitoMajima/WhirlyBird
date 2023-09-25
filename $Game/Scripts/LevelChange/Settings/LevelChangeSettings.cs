using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public record LevelChangeSettings : ILevelChangeSettings
{
    [JsonProperty]
    public IReadOnlyList<ILevelChangeUniqueSettings> LevelChanges { get; }

    [JsonConstructor]
    public LevelChangeSettings (IReadOnlyList<LevelChangeUniqueSettings> levelChanges)
    {
        LevelChanges = levelChanges;
    }
}