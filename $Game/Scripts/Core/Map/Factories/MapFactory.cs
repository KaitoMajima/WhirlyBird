using Godot;

public static class MapFactory
{
    public static IMapModel CreateMapModel (
        ITimeProvider timeProvider, 
        MapSettingsResource mapSettingsResource
    )
    {
        IMapUICanvasModel mapUICanvasModel = CreateMapUICanvasModel(timeProvider);
        IMapWorld2DModel mapWorld2DModel = CreateMapWorld2DModel(mapSettingsResource);
        IMapInputDetectionModel mapInputDetectionModel = CreateMapInputDetectionModel(mapUICanvasModel.PauseModel);

        return new MapModel(mapUICanvasModel, mapWorld2DModel, mapInputDetectionModel);
    }

    static IMapUICanvasModel CreateMapUICanvasModel (ITimeProvider timeProvider)
    {
        IPauseModel pauseModel = PauseFactory.CreatePauseModel(timeProvider);
        return new MapUICanvasModel(pauseModel);
    }

    static IMapWorld2DModel CreateMapWorld2DModel (MapSettingsResource mapSettingsResource)
    {
        IPlayerSettings playerSettings = PlayerFactory.CreatePlayerSettings(mapSettingsResource);
        IPlayerModel playerModel = PlayerFactory.CreatePlayerModel(playerSettings);
        IPillarManagerModel pillarManagerModel = PillarFactory.CreatePillarManagerModel();
        
        return new MapWorld2DModel(
            playerModel, 
            pillarManagerModel
        );
    }

    static IMapInputDetectionModel CreateMapInputDetectionModel (IPauseModel pauseModel) 
        => new MapInputDetectionModel(pauseModel);
}