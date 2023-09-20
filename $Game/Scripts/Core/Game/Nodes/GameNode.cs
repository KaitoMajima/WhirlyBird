using Godot;

public partial class GameNode : Node, IGameNode
{
    [Export]
    public ConfigResource ConfigResource { get; private set; }
}