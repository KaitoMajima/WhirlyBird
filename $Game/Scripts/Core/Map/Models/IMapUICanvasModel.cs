using System;

public interface IMapUICanvasModel : IDisposable
{
    IPauseModel PauseModel { get; }
    IScoreCounterModel ScoreCounterModel { get; }
    IGameOverModel GameOverModel { get; }
    
    void Initialize ();
}