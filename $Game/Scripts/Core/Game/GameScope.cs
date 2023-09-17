using Godot;
using TapNFlap.Core.Utils;

namespace TapNFlap.Core.Game;

public partial class GameScope : SingletonNode<GameScope>
{
    public override void _Ready ()
    {
        base._Ready();
        GD.Print($"Initializing {nameof(GameScope)}!");
    }
}