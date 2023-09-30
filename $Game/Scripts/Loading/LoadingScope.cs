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
    public ILoadingSystem LoadingSystem { get; private set; }
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
        LoadingSystem.StartLoad(scenePath, unloadNode);
    }

    void SetupModels ()
    {
        LoadingSystem = LoadingFactory.CreateLoadingSystem();
    }
    
    void SetupNodes ()
    {
        LoadingNode = LoadingFactory.CreateLoadingNode(this, LoadingSystem);
    }
    
    void InitializeModels ()
    {
        LoadingSystem.Setup(LoadingConfigResource);
    }
    
    void InitializeNodes ()
    {
        LoadingNode.Initialize();
    }
    
    void DisposeModels ()
    {
        LoadingSystem.Dispose();
    }

    void DisposeNodes ()
    {
        LoadingNode.Dispose();
    }
}