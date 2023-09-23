using Godot;

public class PillarManagerModel : IPillarManagerModel
{
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
        GD.Print("Spawning!");
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