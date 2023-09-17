using TapNFlap.Core.Config;
using TapNFlap.Core.Game.Resources;

namespace TapNFlap.Core.Game.Nodes;

public interface IGameNode
{
    GameSettingsResource GameSettingsResource { get;  }
    ConfigResource ConfigResource { get; }
}