using Godot;

public partial class GameNode : Node, IGameNode
{
    [Export]
    public GameSettingsResource GameSettingsResource { get; private set; }
    
    [Export]
    public ConfigResource ConfigResource { get; private set; }
}