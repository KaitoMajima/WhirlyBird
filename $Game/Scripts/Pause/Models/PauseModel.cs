using System;

public class PauseModel : IPauseModel
{
    public event Action OnPauseTriggered;
    public bool IsPaused { get; private set; }

    readonly IGameStateProvider gameStateProvider;
    readonly ITimeProvider timeProvider;

    public PauseModel (IGameStateProvider gameStateProvider, ITimeProvider timeProvider)
    {
        this.gameStateProvider = gameStateProvider;
        this.timeProvider = timeProvider;
    }
        
    public void SetPause (bool pauseState)
    {
        IsPaused = pauseState;
        SetTimeScale();
        gameStateProvider.CallGamePause(pauseState);
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