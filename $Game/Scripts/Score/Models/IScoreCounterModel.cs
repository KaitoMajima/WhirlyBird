using System;

public interface IScoreCounterModel : IDisposable
{
    event Action OnScoreDetected;

    int Score { get; }
    int Highscore { get; }

    void Initialize ();
}