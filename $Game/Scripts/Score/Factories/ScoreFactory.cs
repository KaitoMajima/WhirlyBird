public static class ScoreFactory
{
    public static IScoreCounterModel CreateScoreCounterModel (
        IScoreData scoreData,
        IMainGameSavingSystem saveSystem,
        IPlayerModel playerModel, 
        IGameOverModel gameOverModel
    ) => new ScoreCounterModel(scoreData, saveSystem, playerModel, gameOverModel);
}