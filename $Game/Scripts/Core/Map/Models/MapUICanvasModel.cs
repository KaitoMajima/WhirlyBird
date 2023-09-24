public class MapUICanvasModel : IMapUICanvasModel
{
    public IPauseModel PauseModel { get; }
    public IScoreCounterModel ScoreCounterModel { get; }
    public IGameOverModel GameOverModel { get; }

    public MapUICanvasModel (
        IPauseModel pauseModel, 
        IScoreCounterModel scoreCounterModel,
        IGameOverModel gameOverModel
    )
    {
        PauseModel = pauseModel;
        ScoreCounterModel = scoreCounterModel;
        GameOverModel = gameOverModel;
    }
    
    public void Initialize ()
    {
        ScoreCounterModel.Initialize();
        GameOverModel.Intialiize();
    }
    
    public void Dispose ()
    {
        ScoreCounterModel.Dispose();
        GameOverModel.Dispose();
    }
}