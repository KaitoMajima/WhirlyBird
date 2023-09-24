public static class MapFactory
{
    public static IMapModel CreateMapModel (
        ITimeProvider timeProvider, 
        IRandomProvider randomProvider,
        MapSettingsResource mapSettingsResource
    )
    {
        IMapWorld2DModel mapWorld2DModel = CreateMapWorld2DModel(randomProvider, mapSettingsResource);
        IMapUICanvasModel mapUICanvasModel = CreateMapUICanvasModel(timeProvider, mapWorld2DModel.PlayerModel);
        IMapInputDetectionModel mapInputDetectionModel = CreateMapInputDetectionModel(mapUICanvasModel.PauseModel);
        
        SetupMapWorld2DModel(mapWorld2DModel.PillarManagerModel, mapUICanvasModel.ScoreCounterModel);

        return new MapModel(mapUICanvasModel, mapWorld2DModel, mapInputDetectionModel);
    }

    static IMapUICanvasModel CreateMapUICanvasModel (ITimeProvider timeProvider, IPlayerModel playerModel)
    {
        IPauseModel pauseModel = PauseFactory.CreatePauseModel(timeProvider);
        IGameOverModel gameOverModel = GameOverFactory.CreateGameOverModel(playerModel);
        IScoreCounterModel scoreCounterModel = ScoreFactory.CreateScoreCounterModel(playerModel, gameOverModel);
        return new MapUICanvasModel(pauseModel, scoreCounterModel, gameOverModel);
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

    static void SetupMapWorld2DModel (
        IPillarManagerModel pillarManagerModel, 
        IScoreCounterModel scoreCounterModel
    )
    {
        PillarFactory.SetupPillarManagerModel(pillarManagerModel, scoreCounterModel);
    }
}