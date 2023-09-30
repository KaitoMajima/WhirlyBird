public static class MusicFactory
{
    public static IMusicManagerSystem CreateMusicManagerSystem () 
        => new MusicManagerSystem();

    public static void SetupMusicManagerSystem (
        IMusicManagerSystem musicManagerSystem, 
        IGameStateProvider gameStateProvider,
        MusicResource musicResource
    ) => musicManagerSystem.Setup(gameStateProvider, musicResource);
}