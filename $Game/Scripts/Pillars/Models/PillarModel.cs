using System;

public class PillarModel : IPillarModel
{
    public event Action OnPillarMarkedForDestruction;
    
    ITimer timer;
    double destructionSeconds;
    
    public void Setup (double secondsUntilDestruction)
    {
        destructionSeconds = secondsUntilDestruction;
    }

    public void SetTimer (ITimer timer)
    {
        this.timer = timer;
    }

    public void Initialize ()
    {
        AddTimerListeners();
        timer.WaitTime = destructionSeconds;
        timer.Start();
    }
    
    void HandleTimerTimeout ()
    {
        OnPillarMarkedForDestruction?.Invoke();
    }

    void AddTimerListeners ()
    {
        timer.Timeout += HandleTimerTimeout;
    }
    
    void RemoveTimerListeners ()
    {
        timer.Timeout -= HandleTimerTimeout;
    }

    public void Dispose ()
    {
        RemoveTimerListeners();
    }
}