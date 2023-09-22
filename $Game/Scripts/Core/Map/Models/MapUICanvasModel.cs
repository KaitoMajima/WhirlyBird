public class MapUICanvasModel : IMapUICanvasModel
{
    public IPauseModel PauseModel { get; }

    public MapUICanvasModel (IPauseModel pauseModel)
    {
        PauseModel = pauseModel;
    }
    
    public void Initialize ()
    {
        PauseModel.Initialize();
    }
    
    public void Dispose ()
    {
        PauseModel.Dispose();
    }
}