public static class PauseFactory
{
    public static IPauseModel CreatePauseModel (ITimeProvider timeProvider) 
        => new PauseModel(timeProvider);
}