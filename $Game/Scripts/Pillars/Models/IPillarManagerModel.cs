using System;
using Godot;

public interface IPillarManagerModel : IDisposable
{
    event Action OnPillarSpawn;
    
    void StartTimedSpawning (Timer timer);
}