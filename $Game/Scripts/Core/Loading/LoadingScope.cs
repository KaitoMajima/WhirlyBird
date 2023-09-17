using Godot;
using TapNFlap.Core.Utils;

namespace TapNFlap.Core.Loading;

public partial class LoadingScope : SingletonNode<LoadingScope>
{
    public override void _Ready ()
    {
        base._Ready();
        GD.Print($"Initializing {nameof(LoadingScope)}!");
    }
}