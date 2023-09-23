using Godot;

public partial class LoadingScope : SingletonNode
{
    public static LoadingScope Instance { get; private set; }
    
    #region Resources
    LoadingConfigResource LoadingConfigResource => LoadingNode.LoadingConfigResource;
    #endregion
    
    #region Nodes
    public LoadingNode LoadingNode { get; private set; }
    #endregion

    #region Models
    public ILoadingModel LoadingModel { get; private set; }
    #endregion

    bool isLoading;
    PackedScene sceneToLoad;
    Node nodeToUnload;
    
    public override void _Ready ()
    {
        base._Ready();
        Instance = RegisterSingletonInstance(this);
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

    public void Load (string scenePath, Node unloadNode = null)
    {
        LoadingModel.StartLoad(scenePath, unloadNode);
    }

    void SetupModels ()
    {
        LoadingModel = LoadingFactory.CreateLoadingModel();
    }
    
    void SetupNodes ()
    {
        LoadingNode = LoadingFactory.CreateLoadingNode(this, LoadingModel);
    }
    
    void InitializeModels ()
    {
        LoadingModel.Setup(LoadingConfigResource);
    }
    
    void InitializeNodes ()
    {
        LoadingNode.Initialize();
    }
    
    void DisposeModels ()
    {
        LoadingModel.Dispose();
    }

    void DisposeNodes ()
    {
        LoadingNode.Dispose();
    }
}