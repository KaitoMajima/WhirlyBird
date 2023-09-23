using System;
using Godot;

public interface IPillarManagerModel : IDisposable
{
    void StartTimedSpawning (Timer timer);
}