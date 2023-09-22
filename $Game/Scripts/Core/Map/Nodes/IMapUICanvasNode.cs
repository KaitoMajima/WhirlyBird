using System;

public interface IMapUICanvasNode : IDisposable
{
    IPauseNode PauseNode { get; }
    
    void Setup (IPauseModel pauseModel);
    void Initialize ();
}