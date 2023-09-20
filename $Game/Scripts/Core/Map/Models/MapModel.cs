public class MapModel : IMapModel
{
    public IMapUICanvasModel MapUICanvasModel { get; }
    public IMapWorld2DModel MapWorld2DModel { get; }

    public MapModel (
        IMapUICanvasModel mapUICanvasModel, 
        IMapWorld2DModel mapWorld2DModel
    )
    {
        MapUICanvasModel = mapUICanvasModel;
        MapWorld2DModel = mapWorld2DModel;
    }
    
    public void Initialize ()
    {
        MapUICanvasModel.Initialize();
        MapWorld2DModel.Initialize();
    }

    public void Dispose ()
    {
        MapUICanvasModel.Dispose();
        MapWorld2DModel.Dispose();
    }
}