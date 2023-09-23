using System;
using Godot;

public class PillarManagerModel : IPillarManagerModel
{
    public event Action OnPillarSpawn;
    
    Timer currentTimer;
    
    public void StartTimedSpawning (Timer timer)
    {
        RemoveTimerListeners();
        currentTimer = timer;
        currentTimer.Start();
        AddTimerListeners();
    }

    void HandleTimerTimeout ()
    {
        OnPillarSpawn?.Invoke();
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

    public void Dispose ()
    {
        RemoveTimerListeners();
    }
}