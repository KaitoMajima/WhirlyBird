public class MainGameSavingSystem : BaseGameSavingSystem<IGameSaveData, GameSaveData>, IMainGameSavingSystem
{
    public MainGameSavingSystem (
        ICryptographer cryptographer,
        ISerializer serializer
    ) : base(cryptographer, serializer)
    {
        
    }
}