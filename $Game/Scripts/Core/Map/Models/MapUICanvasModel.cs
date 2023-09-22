public class MapUICanvasModel : IMapUICanvasModel
{
    public IPauseModel PauseModel { get; }

    public MapUICanvasModel (IPauseModel pauseModel)
    {
        PauseModel = pauseModel;
    }
    
    public void Initialize ()
    {
        
    }
    
    public void Dispose ()
    {
        
    }
}