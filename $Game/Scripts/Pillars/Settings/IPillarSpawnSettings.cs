using System.Collections.Generic;

public interface IPillarSpawnSettings
{
    IReadOnlyList<IPillarSettings> PillarDifficulty { get; }
    double PillarSecondsUntilDestruction { get; }
    float PillarSpawnMinYHeight { get; }
    float PillarSpawnMaxYHeight { get; }
    float ParallaxBaseValue { get; }
}