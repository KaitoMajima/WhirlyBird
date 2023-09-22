public class MapModel : IMapModel
{
    public IMapUICanvasModel MapUICanvasModel { get; }
    public IMapWorld2DModel MapWorld2DModel { get; }
    public IMapInputDetectionModel MapInputDetectionModel { get; }

    public MapModel (
        IMapUICanvasModel mapUICanvasModel, 
        IMapWorld2DModel mapWorld2DModel,
        IMapInputDetectionModel mapInputDetectionModel
    )
    {
        MapUICanvasModel = mapUICanvasModel;
        MapWorld2DModel = mapWorld2DModel;
        MapInputDetectionModel = mapInputDetectionModel;
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