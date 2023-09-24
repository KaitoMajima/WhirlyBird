﻿using System.Collections.Generic;
using Newtonsoft.Json;

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

    [JsonConstructor]
    public PillarSpawnSettings (
        IReadOnlyList<PillarSettings> pillarDifficulty,
        double pillarSecondsUntilDestruction,
        float pillarSpawnMinYHeight,
        float pillarSpawnMaxYHeight
    )
    {
        PillarDifficulty = pillarDifficulty;
        PillarSecondsUntilDestruction = pillarSecondsUntilDestruction;
        PillarSpawnMinYHeight = pillarSpawnMinYHeight;
        PillarSpawnMaxYHeight = pillarSpawnMaxYHeight;
    }
}