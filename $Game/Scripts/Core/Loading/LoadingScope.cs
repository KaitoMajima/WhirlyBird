using Godot;
using TapNFlap.Core.Utils;
using static TapNFlap.Core.Utils.GlobalSettings.Paths.Loading;

namespace TapNFlap.Core.Loading;

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