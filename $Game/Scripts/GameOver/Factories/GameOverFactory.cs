public static class GameOverFactory
{
    public static IGameOverModel CreateGameOverModel (IPlayerModel playerModel)
        => new GameOverModel(playerModel);
}