using System;
using Godot;

public interface IPillarModel
{
    event Action OnPillarMarkedForDestruction;
    
    void StartTimedDestruction (Timer timer);
}