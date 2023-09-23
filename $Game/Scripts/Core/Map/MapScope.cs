using Godot;

public partial class MapScope : Node
{
    [Export] NodePath mapNodePath;
    [Export] NodePath mapUICanvasNodePath;
    [Export] NodePath mapWorld2DNodePath;
    [Export] NodePath mapInputDetectionNodePath;
    
    public IMapModel MapModel { get; private set; }
    public IMapUICanvasModel MapUICanvasModel => MapModel.MapUICanvasModel;
    public IMapWorld2DModel MapWorld2DModel => MapModel.MapWorld2DModel;
    public IMapInputDetectionModel MapInputDetectionModel => MapModel.MapInputDetectionModel;
    
    [Export]
    public MapNode MapNode { get; private set; }
    public MapUICanvasNode MapUICanvasNode => MapNode.MapUICanvasNode;
    public MapWorld2DNode MapWorld2DNode => MapNode.MapWorld2DNode;
    public MapInputDetectionNode MapInputDetectionNode => MapNode.MapInputDetectionNode;
    
    [Export]
    public MapSettingsResource MapSettingsResource { get; private set; }
    
    GameScope GameScope => GameScope.Instance;
    
    public override void _Ready ()
    {
        SetupModels();
        SetupNodes();
        InitializeModels();
        InitializeNodes();
    }

    public override void _ExitTree ()
    {
        DisposeModels();
        DisposeNodes();
    }

    void SetupModels ()
    {
        MapModel = MapFactory.CreateMapModel(
            GameScope.TimeProvider, 
            MapSettingsResource
        );
    }
    
    void SetupNodes ()
    {
        MapNode.Setup(
            MapUICanvasModel.PauseModel, 
            MapWorld2DModel.PlayerModel, 
            MapInputDetectionModel, 
            MapWorld2DModel.PillarManagerModel
        );
    }
    
    void InitializeModels ()
    {
        MapModel.Initialize();
    }
    
    void InitializeNodes ()
    {
        MapNode.Initialize();
    }
    
    void DisposeModels ()
    {
        MapModel.Dispose();
    }
    
    void DisposeNodes ()
    {
        MapNode.Dispose();
    }
}