using System;

public interface IMapUICanvasModel : IDisposable
{
    IPauseModel PauseModel { get; }
    void Initialize ();
}