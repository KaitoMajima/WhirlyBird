using System;
using Godot;

public interface IPillarModel : IDisposable
{
    event Action OnPillarMarkedForDestruction;

    void Setup (double secondsUntilDestruction);
    void SetTimer (ITimer timer);

    void Initialize ();
}