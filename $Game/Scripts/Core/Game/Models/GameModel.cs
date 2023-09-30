public class GameModel : IGameModel
{
    public IMusicManagerSystem MusicManagerSystem { get; }

    public GameModel (IMusicManagerSystem musicManagerSystem)
    {
        MusicManagerSystem = musicManagerSystem;
    }

    public void Initialize ()
    {
        MusicManagerSystem.Initialize();
    }

    public void Dispose ()
    {
        MusicManagerSystem.Dispose();
    }
}