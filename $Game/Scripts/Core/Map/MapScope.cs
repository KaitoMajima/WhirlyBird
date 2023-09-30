using Godot;

public partial class MapScope : Node
{
    public IMapModel MapModel { get; private set; }
    
    [Export]
    public MapNode MapNode { get; private set; }
    
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
            GameScope.GameSaveData,
            GameScope.GameSavingSystem,
            GameScope.TimeProvider, 
            GameScope.RandomProvider,
            GameScope.GameModel.MusicManagerSystem,
            MapSettingsResource,
            GameScope.GameStateProvider
        );
    }
    
    void SetupNodes ()
    {
        MapFactory.SetupMapNode(
            MapNode, 
            MapModel, 
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