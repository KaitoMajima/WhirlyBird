using System;
using Godot;

public interface IPillarManagerModel : IDisposable
{
    event Action OnPillarSpawn;
    event Action OnPillarDifficultyChanged;
    
    float PillarSpeed { get; }
    double PillarSecondsUntilDestruction { get; }

    Vector2 GetNewRandomSpawningPoint ();

    void Setup (IScoreCounterModel scoreCounterModel);
    void Initialize ();
    void StartTimedSpawning (Timer timer);
}