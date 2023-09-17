using Godot;
using TapNFlap.Core.Config;
using TapNFlap.Core.Game.Resources;

namespace TapNFlap.Core.Game.Nodes;

public partial class GameNode : Node, IGameNode
{
    [Export]
    public GameSettingsResource GameSettingsResource { get; private set; }
    
    [Export]
    public ConfigResource ConfigResource { get; private set; }
}