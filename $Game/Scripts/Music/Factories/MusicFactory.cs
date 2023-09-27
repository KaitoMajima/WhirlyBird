public static class MusicFactory
{
    public static IMusicManagerModel CreateMusicManagerModel () 
        => new MusicManagerModel();

    public static void SetupMusicManagerModel (
        IMusicManagerModel musicManagerModel, 
        IGameStateProvider gameStateProvider,
        MusicResource musicResource
    ) => musicManagerModel.Setup(gameStateProvider, musicResource);
}