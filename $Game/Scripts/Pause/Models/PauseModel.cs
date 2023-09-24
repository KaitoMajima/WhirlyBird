using System;

public class PauseModel : IPauseModel
{
    public event Action OnPauseTriggered;
    public bool IsPaused { get; private set; }

    readonly ITimeProvider timeProvider;

    public PauseModel (ITimeProvider timeProvider)
    {
        this.timeProvider = timeProvider;
    }
        
    public void SetPause (bool pauseState)
    {
        IsPaused = pauseState;
        SetTimeScale();
        OnPauseTriggered?.Invoke();
    }

    public void PauseToggle ()
    {
        IsPaused = !IsPaused;
        SetTimeScale();
        OnPauseTriggered?.Invoke();
    }

    void SetTimeScale ()
    {
        timeProvider.TimeScale = IsPaused ? 0 : 1;
    }
}