public static class ScoreFactory
{
    public static IScoreCounterModel CreateScoreCounterModel (IPlayerModel playerModel) 
        => new ScoreCounterModel(playerModel);
}