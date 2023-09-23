using System;
using Godot;

public class PillarModel : IPillarModel
{
    public event Action OnPillarMarkedForDestruction;
    
    Timer currentTimer;
    
    public void StartTimedDestruction (Timer timer)
    {
        RemoveTimerListeners();
        currentTimer = timer;
        currentTimer.Start();
        AddTimerListeners();
    }
    
    void HandleTimerTimeout ()
    {
        OnPillarMarkedForDestruction?.Invoke();
    }

    void AddTimerListeners ()
    {
        currentTimer.Timeout += HandleTimerTimeout;
    }
    
    void RemoveTimerListeners ()
    {
        if (currentTimer != null)
            currentTimer.Timeout -= HandleTimerTimeout;
    }
}