using System;

public interface IPauseModel
{
    event Action OnPauseTriggered;
    bool IsPaused { get; }

    void SetPause (bool pauseState);
    void PauseToggle ();
}