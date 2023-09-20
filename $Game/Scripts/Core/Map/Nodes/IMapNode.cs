using System;

public interface IMapNode : IDisposable
{
    IMapUICanvasNode MapUICanvasNode { get; }
    IMapWorld2DNode MapWorld2DNode { get; }
    
    void Setup (IMapUICanvasNode mapUICanvasNode, IMapWorld2DNode mapWorld2DNode);
    void Initialize ();
}