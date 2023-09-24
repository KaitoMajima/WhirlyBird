using System;

public interface IScoreCounterModel : IDisposable
{
    event Action OnScoreDetected;

    int Score { get; }

    void Initialize ();
}