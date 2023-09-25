public class GameModel : IGameModel
{
    public IMusicManagerModel MusicManagerModel { get; }

    public GameModel (IMusicManagerModel musicManagerModel)
    {
        MusicManagerModel = musicManagerModel;
    }

    public void Initialize ()
    {
        
    }

    public void Dispose ()
    {
        MusicManagerModel.Dispose();
    }
}