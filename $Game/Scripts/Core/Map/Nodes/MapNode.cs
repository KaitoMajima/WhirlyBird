using Godot;

public partial class MapNode : Node, IMapNode
{
    public IMapUICanvasNode MapUICanvasNode { get; private set; }
    public IMapWorld2DNode MapWorld2DNode { get; private set; }
    
    public void Setup (IMapUICanvasNode mapUICanvasNode, IMapWorld2DNode mapWorld2DNode)
    {
        MapUICanvasNode = mapUICanvasNode;
        MapWorld2DNode = mapWorld2DNode;
    }
    
    public void Initialize ()
    {
        MapUICanvasNode.Initialize();
        MapWorld2DNode.Initialize();
    }

    public new void Dispose ()
    {
        MapUICanvasNode.Dispose();
        MapWorld2DNode.Dispose();
        base.Dispose();
    }
}