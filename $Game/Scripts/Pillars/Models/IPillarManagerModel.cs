using System;
using Godot;

public interface IPillarManagerModel : IDisposable
{
    event Action OnPillarSpawn;
    
    int PillarId { get; }
    float PillarSpeed { get; }
    double PillarSecondsUntilDestruction { get; }

    Vector2 GetNewRandomSpawningPoint ();
    
    void StartTimedSpawning (Timer timer);
}