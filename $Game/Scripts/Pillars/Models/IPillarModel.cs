using System;
using Godot;

public interface IPillarModel
{
    event Action OnPillarMarkedForDestruction;

    void SetSecondsUntilDestruction (double destructionSeconds);
    void StartTimedDestruction (Timer timer);
}