using Godot;

public partial class LoadingScope : SingletonNode<LoadingScope>
{
    #region Nodes
    public ILoadingNode LoadingNode { get; private set; }
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
        CreateModels();
        CreateNodes();
        InitializeNodes();
    }

    public override void _ExitTree ()
    {
        DisposeNodes();
    }

    public void Load (PackedScene scene, Node unloadNode = null)
    {
        LoadingModel.SetupLoad(scene, unloadNode);
    }

    void CreateModels ()
    {
        LoadingModel = LoadingFactory.CreateLoadingModel();
    }
    
    void CreateNodes ()
    {
        LoadingNode = LoadingFactory.CreateLoadingNode(this, LoadingModel);
    }
    
    void InitializeNodes ()
    {
        LoadingNode.Initialize();
    }

    void DisposeNodes ()
    {
        LoadingNode.Dispose();
    }
}