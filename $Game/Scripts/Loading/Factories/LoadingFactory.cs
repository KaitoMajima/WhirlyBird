using Godot;
using static GlobalSettings.Paths.Loading;

public static class LoadingFactory
{
    public static ILoadingSystem CreateLoadingSystem () 
        => new LoadingSystem();

    public static LoadingNode CreateLoadingNode (Node callerNode, ILoadingSystem loadingSystem)
    {
        PackedScene loadingNodeScene = GD.Load<PackedScene>(LOADING_NODE_SCENE_PATH);
        LoadingNode loadingNode = loadingNodeScene.Instantiate<LoadingNode>();
        loadingNode.Setup(loadingSystem);
        callerNode.AddChild(loadingNode);
        return loadingNode;
    }
}