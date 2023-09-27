using System;

public class GameStateProvider : IGameStateProvider
{
    public event Action OnGamePauseTriggered;
    
    public bool IsGamePaused { get; private set; }
    
    public void CallGamePause (bool pause)
    {
        IsGamePaused = pause;
        OnGamePauseTriggered?.Invoke();
    }
}