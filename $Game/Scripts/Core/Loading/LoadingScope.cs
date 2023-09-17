using Godot;
using static GlobalSettings.Paths.Loading;

public partial class LoadingScope : SingletonNode<LoadingScope>
{
    public override void _Ready ()
    {
        base._Ready();
        PackedScene loadingNodeScene = GD.Load<PackedScene>(LOADING_NODE_SCENE_PATH);
        Node loadingNode = loadingNodeScene.Instantiate();
        AddChild(loadingNode);
    }
}