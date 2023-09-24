public static class ScoreFactory
{
    public static IScoreCounterModel CreateScoreCounterModel (
        IPlayerModel playerModel, 
        IGameOverModel gameOverModel
    ) => new ScoreCounterModel(playerModel, gameOverModel);
}