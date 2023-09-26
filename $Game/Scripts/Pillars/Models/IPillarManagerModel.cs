using System;
using Godot;

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

    Vector2 GetNewRandomSpawningPoint ();

    void Setup (IScoreCounterModel scoreCounterModel);
    void Initialize ();
    void StartTimedSpawning (Timer timer);
}