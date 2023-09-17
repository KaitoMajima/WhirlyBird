using Godot;
using TapNFlap.Core.Utils;
using static TapNFlap.Core.Utils.GlobalSettings.Paths.Game;

namespace TapNFlap.Core.Game;

public partial class GameScope : SingletonNode<GameScope>
{
    public override void _Ready ()
    {
        base._Ready();
        PackedScene gameNodeScene = GD.Load<PackedScene>(GAME_NODE_SCENE_PATH);
        Node gameNode = gameNodeScene.Instantiate();
        AddChild(gameNode);
    }
}