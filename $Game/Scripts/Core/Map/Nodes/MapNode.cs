using Godot;

public partial class MapNode : Node
{
    [Export]
    public MapUICanvasNode MapUICanvasNode { get; private set; }
    
    [Export]
    public MapWorld2DNode MapWorld2DNode { get; private set; }
    
    [Export]
    public MapInputDetectionNode MapInputDetectionNode { get; private set; }
    
    public void Setup (
        IPauseModel pauseModel,
        IPlayerModel playerModel,
        IMapInputDetectionModel mapInputDetectionModel,
        IPillarManagerModel pillarManagerModel,
        IScoreCounterModel scoreCounterModel
    )
    {
        MapUICanvasNode.Setup(pauseModel, scoreCounterModel);
        MapWorld2DNode.Setup(
            playerModel,
            mapInputDetectionModel, 
            pillarManagerModel
        );
        MapInputDetectionNode.Setup(mapInputDetectionModel);
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