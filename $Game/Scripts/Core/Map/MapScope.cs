using Godot;

public partial class MapScope : Node
{
    #region Node Paths
    [Export] NodePath mapNodePath;
    [Export] NodePath mapUICanvasNodePath;
    [Export] NodePath mapWorld2DNodePath;
    [Export] NodePath mapInputDetectionNodePath;
    #endregion

    #region Models
    public IMapModel MapModel { get; private set; }
    public IMapUICanvasModel MapUICanvasModel => MapModel.MapUICanvasModel;
    public IMapWorld2DModel MapWorld2DModel => MapModel.MapWorld2DModel;
    public IMapInputDetectionModel MapInputDetectionModel => MapModel.MapInputDetectionModel;
    #endregion
    
    #region Nodes
    public IMapNode MapNode { get; private set; }
    public IMapUICanvasNode MapUICanvasNode => MapNode.MapUICanvasNode;
    public IMapWorld2DNode MapWorld2DNode => MapNode.MapWorld2DNode;
    public IMapInputDetectionNode MapInputDetectionNode => MapNode.MapInputDetectionNode;
    #endregion
    
    #region Resources
    [Export]
    public MapSettingsResource MapSettingsResource { get; private set; }
    #endregion
    
    #region Scopes
    GameScope GameScope => GameScope.Instance;
    #endregion
    
    public override void _Ready ()
    {
        CreateModels();
        CreateNodes();
        InitializeModels();
        InitializeNodes();
    }

    public override void _ExitTree ()
    {
        DisposeModels();
        DisposeNodes();
    }

    void CreateModels ()
    {
        MapModel = MapFactory.CreateMapModel(
            GameScope.TimeProvider, 
            MapSettingsResource
        );
    }
    
    void CreateNodes ()
    {
        MapNode = MapFactory.CreateMapNode(
            this, 
            mapNodePath, 
            mapUICanvasNodePath, 
            mapWorld2DNodePath,
            mapInputDetectionNodePath,
            MapUICanvasModel,
            MapWorld2DModel,
            MapInputDetectionModel
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