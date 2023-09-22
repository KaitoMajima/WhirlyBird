using System;

public interface IMapUICanvasNode : IDisposable
{
    void Setup (IPauseModel pauseModel);
    void Initialize ();
}