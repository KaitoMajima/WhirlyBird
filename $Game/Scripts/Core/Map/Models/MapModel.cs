public class MapModel : IMapModel
{
    public IMapUICanvasModel MapUICanvasModel { get; }
    public IMapWorld2DModel MapWorld2DModel { get; }
    public IMapInputDetectionModel MapInputDetectionModel { get; }

    readonly IMusicManagerModel musicManagerModel;
    
    public MapModel (
        IMapUICanvasModel mapUICanvasModel, 
        IMapWorld2DModel mapWorld2DModel,
        IMapInputDetectionModel mapInputDetectionModel,
        IMusicManagerModel musicManagerModel
    )
    {
        this.musicManagerModel = musicManagerModel;
        MapUICanvasModel = mapUICanvasModel;
        MapWorld2DModel = mapWorld2DModel;
        MapInputDetectionModel = mapInputDetectionModel;
    }
    
    public void Initialize ()
    {
        MapUICanvasModel.Initialize();
        MapWorld2DModel.Initialize();
        musicManagerModel.Play(MusicClipType.Level0);
    }

    public void Dispose ()
    {
        MapUICanvasModel.Dispose();
        MapWorld2DModel.Dispose();
    }
}