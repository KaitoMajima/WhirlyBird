using System;

public interface IGameStateProvider
{
    event Action OnGamePauseTriggered;
    
    bool IsGamePaused { get; }

    void CallGamePause (bool pause);
}