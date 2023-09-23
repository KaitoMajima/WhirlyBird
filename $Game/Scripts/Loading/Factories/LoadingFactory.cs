using Godot;
using static GlobalSettings.Paths.Loading;

public static class LoadingFactory
{
    public static ILoadingModel CreateLoadingModel () 
        => new LoadingModel();

    public static LoadingNode CreateLoadingNode (Node callerNode, ILoadingModel loadingModel)
    {
        PackedScene loadingNodeScene = GD.Load<PackedScene>(LOADING_NODE_SCENE_PATH);
        LoadingNode loadingNode = loadingNodeScene.Instantiate<LoadingNode>();
        loadingNode.Setup(loadingModel);
        callerNode.AddChild(loadingNode);
        return loadingNode;
    }
}