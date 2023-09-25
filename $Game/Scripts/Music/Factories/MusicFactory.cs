public static class MusicFactory
{
    public static IMusicManagerModel CreateMusicManagerModel () 
        => new MusicManagerModel();

    public static void SetupMusicManagerModel (IMusicManagerModel musicManagerModel, MusicResource musicResource)
        => musicManagerModel.Setup(musicResource);
}