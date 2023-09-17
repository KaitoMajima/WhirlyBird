using TapNFlap.Core.Saving.Cryptography;
using TapNFlap.Core.Saving.Data;
using TapNFlap.Core.Saving.Serialization;

namespace TapNFlap.Core.Saving;

public class MainGameSavingSystem : BaseGameSavingSystem<IGameSaveData, GameSaveData>, IMainGameSavingSystem
{
    public MainGameSavingSystem (
        ICryptographer cryptographer,
        ISerializer serializer
    ) : base(cryptographer, serializer)
    {
        
    }
}