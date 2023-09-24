public class MapUICanvasModel : IMapUICanvasModel
{
    public IPauseModel PauseModel { get; }
    public IScoreCounterModel ScoreCounterModel { get; }

    public MapUICanvasModel (
        IPauseModel pauseModel, 
        IScoreCounterModel scoreCounterModel
    )
    {
        PauseModel = pauseModel;
        ScoreCounterModel = scoreCounterModel;
    }
    
    public void Initialize ()
    {
        ScoreCounterModel.Initialize();
    }
    
    public void Dispose ()
    {
        ScoreCounterModel.Dispose();
    }
}