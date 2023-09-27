using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public record PillarSpawnSettings : IPillarSpawnSettings
{
    [JsonProperty] 
    public IReadOnlyList<IPillarSettings> PillarDifficulty { get; }

    [JsonProperty]
    public double PillarSecondsUntilDestruction { get; }
    
    [JsonProperty]
    public float PillarSpawnMinYHeight { get; }
    
    [JsonProperty]
    public float PillarSpawnMaxYHeight { get; }

    [JsonProperty]
    public float ParallaxBaseValue { get; }
    
    [JsonProperty]
    public int InitialPillarsPassed { get; }

    [JsonConstructor]
    public PillarSpawnSettings (
        IReadOnlyList<PillarSettings> pillarDifficulty,
        double pillarSecondsUntilDestruction,
        float pillarSpawnMinYHeight,
        float pillarSpawnMaxYHeight,
        float parallaxBaseValue,
        int initialPillarsPassed
    )
    {
        PillarDifficulty = pillarDifficulty;
        PillarSecondsUntilDestruction = pillarSecondsUntilDestruction;
        PillarSpawnMinYHeight = pillarSpawnMinYHeight;
        PillarSpawnMaxYHeight = pillarSpawnMaxYHeight;
        ParallaxBaseValue = parallaxBaseValue;
        InitialPillarsPassed = initialPillarsPassed;
    }
}