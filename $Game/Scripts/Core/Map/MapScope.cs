using Godot;

public partial class MapScope : Node
{
    public IMapModel MapModel { get; private set; }
    public IMapWorld2DModel MapWorld2DModel => MapModel.MapWorld2DModel;
    public IMapUICanvasModel MapUICanvasModel => MapModel.MapUICanvasModel;
    public IMapInputDetectionModel MapInputDetectionModel => MapModel.MapInputDetectionModel;
    
    [Export]
    public MapNode MapNode { get; private set; }
    public MapWorld2DNode MapWorld2DNode => MapNode.MapWorld2DNode;
    public MapUICanvasNode MapUICanvasNode => MapNode.MapUICanvasNode;
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
            GameScope.RandomProvider,
            MapSettingsResource
        );
    }
    
    void SetupNodes ()
    {
        MapNode.Setup(
            MapUICanvasModel.PauseModel, 
            MapWorld2DModel.PlayerModel, 
            MapInputDetectionModel, 
            MapWorld2DModel.PillarManagerModel,
            MapUICanvasModel.ScoreCounterModel,
            MapUICanvasModel.GameOverModel,
            GameScope.RandomProvider
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