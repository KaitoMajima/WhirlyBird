using System;

public interface IPillarManagerModel : IDisposable
{
    event Action OnPillarSpawn;
    event Action OnPillarPassed;
    event Action OnPillarDifficultyChanged;
    
    int PillarsPassedCount { get; }
    float PillarSpeed { get; }
    double PillarSecondsUntilDestruction { get; }
    float ParallaxBaseValue { get; }
    float ParallaxMultiplier { get; }

    float GetNewRandomSpawningPoint ();

    void Setup (IScoreCounterModel scoreCounterModel);
    void SetTimer (ITimer timer);
    void Initialize ();

    void StartSpawning ();
}