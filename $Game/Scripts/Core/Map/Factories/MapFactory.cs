public static class MapFactory
{
    public static IMapModel CreateMapModel (
        ITimeProvider timeProvider, 
        IRandomProvider randomProvider,
        MapSettingsResource mapSettingsResource
    )
    {
        IMapUICanvasModel mapUICanvasModel = CreateMapUICanvasModel(timeProvider);
        IMapWorld2DModel mapWorld2DModel = CreateMapWorld2DModel(randomProvider, mapSettingsResource);
        IMapInputDetectionModel mapInputDetectionModel = CreateMapInputDetectionModel(mapUICanvasModel.PauseModel);

        return new MapModel(mapUICanvasModel, mapWorld2DModel, mapInputDetectionModel);
    }

    static IMapUICanvasModel CreateMapUICanvasModel (ITimeProvider timeProvider)
    {
        IPauseModel pauseModel = PauseFactory.CreatePauseModel(timeProvider);
        return new MapUICanvasModel(pauseModel);
    }

    static IMapWorld2DModel CreateMapWorld2DModel (
        IRandomProvider randomProvider, 
        MapSettingsResource mapSettingsResource
    )
    {
        IPlayerSettings playerSettings = PlayerFactory.CreatePlayerSettings(mapSettingsResource);
        IPlayerModel playerModel = PlayerFactory.CreatePlayerModel(playerSettings);
        IPillarSpawnSettings pillarSpawnSettings = PillarFactory.CreatePillarSpawnSettings(mapSettingsResource);
        IPillarManagerModel pillarManagerModel = PillarFactory.CreatePillarManagerModel(
            pillarSpawnSettings, 
            randomProvider
        );
        
        return new MapWorld2DModel(
            playerModel, 
            pillarManagerModel
        );
    }

    static IMapInputDetectionModel CreateMapInputDetectionModel (IPauseModel pauseModel) 
        => new MapInputDetectionModel(pauseModel);
}