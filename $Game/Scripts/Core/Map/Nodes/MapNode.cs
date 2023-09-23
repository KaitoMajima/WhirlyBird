using Godot;

public partial class MapNode : Node, IMapNode
{
    public IMapUICanvasNode MapUICanvasNode { get; private set; }
    public IMapWorld2DNode MapWorld2DNode { get; private set; }
    public IMapInputDetectionNode MapInputDetectionNode { get; private set; }
    
    public void Setup (
        IMapUICanvasNode mapUICanvasNode, 
        IMapWorld2DNode mapWorld2DNode, 
        IMapInputDetectionNode mapInputDetectionNode
    )
    {
        MapUICanvasNode = mapUICanvasNode;
        MapWorld2DNode = mapWorld2DNode;
        MapInputDetectionNode = mapInputDetectionNode;
    }
    
    public void Initialize ()
    {
        MapUICanvasNode.Initialize();
        MapWorld2DNode.Initialize();
        MapInputDetectionNode.Initialize();
    }

    public new void Dispose ()
    {
        MapUICanvasNode.Dispose();
        MapWorld2DNode.Dispose();
        MapInputDetectionNode.Dispose();
        base.Dispose();
    }
}