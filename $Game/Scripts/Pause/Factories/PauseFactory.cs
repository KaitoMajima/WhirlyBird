public static class PauseFactory
{
    public static IPauseModel CreatePauseModel (IGameStateProvider gameStateProvider, ITimeProvider timeProvider) 
        => new PauseModel(gameStateProvider, timeProvider);
}