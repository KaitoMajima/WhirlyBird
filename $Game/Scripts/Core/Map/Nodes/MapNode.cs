using Godot;

public partial class MapNode : Node
{
    [Export]
    public MapUICanvasNode MapUICanvasNode { get; private set; }
    
    [Export]
    public MapWorld2DNode MapWorld2DNode { get; private set; }
    
    [Export]
    public MapInputDetectionNode MapInputDetectionNode { get; private set; }

    IMapModel mapModel;
    
    public void Setup (IMapModel mapModel)
    {
        this.mapModel = mapModel;
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