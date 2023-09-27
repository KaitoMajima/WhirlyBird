public static class MapFactory
{
    public static IMapModel CreateMapModel (
        IGameSaveData saveData,
        IMainGameSavingSystem saveSystem,
        ITimeProvider timeProvider, 
        IRandomProvider randomProvider,
        IMusicManagerModel musicManagerModel,
        MapSettingsResource mapSettingsResource,
        IGameStateProvider gameStateProvider
    )
    {
        IMapWorld2DModel mapWorld2DModel = CreateMapWorld2DModel(randomProvider, mapSettingsResource, musicManagerModel);
        IMapUICanvasModel mapUICanvasModel = CreateMapUICanvasModel(
            saveData, 
            saveSystem, 
            timeProvider, 
            mapWorld2DModel.PlayerModel,
            gameStateProvider
        );
        IMapInputDetectionModel mapInputDetectionModel = CreateMapInputDetectionModel(mapUICanvasModel.PauseModel);
        
        SetupMapWorld2DModel(
            mapWorld2DModel.PillarManagerModel, 
            mapUICanvasModel.ScoreCounterModel
        );

        return new MapModel(
            mapUICanvasModel, 
            mapWorld2DModel, 
            mapInputDetectionModel, 
            musicManagerModel
        );
    }

    static IMapUICanvasModel CreateMapUICanvasModel (
        IGameSaveData saveData, 
        IMainGameSavingSystem saveSystem, 
        ITimeProvider timeProvider, 
        IPlayerModel playerModel,
        IGameStateProvider gameStateProvider
    )
    {
        IPauseModel pauseModel = PauseFactory.CreatePauseModel(gameStateProvider, timeProvider);
        IGameOverModel gameOverModel = GameOverFactory.CreateGameOverModel(playerModel);
        IScoreCounterModel scoreCounterModel = ScoreFactory.CreateScoreCounterModel(
            saveData.ScoreData, 
            saveSystem, 
            playerModel, 
            gameOverModel
        );
        return new MapUICanvasModel(pauseModel, scoreCounterModel, gameOverModel);
    }

    static IMapWorld2DModel CreateMapWorld2DModel (
        IRandomProvider randomProvider, 
        MapSettingsResource mapSettingsResource,
        IMusicManagerModel musicManagerModel
    )
    {
        IPillarSpawnSettings pillarSpawnSettings = PillarFactory.CreatePillarSpawnSettings(mapSettingsResource);
        IPillarManagerModel pillarManagerModel = PillarFactory.CreatePillarManagerModel(
            pillarSpawnSettings, 
            randomProvider
        );
        ILevelChangeSettings levelChangeSettings 
            = LevelChangeFactory.CreateLevelChangeSettings(mapSettingsResource);
        ILevelChangeModel levelChangeModel = LevelChangeFactory.CreateLevelChangeModel(
            levelChangeSettings, 
            pillarManagerModel,
            musicManagerModel
        );
        IParallaxManagerModel parallaxManagerModel =
            ParallaxFactory.CreateParallaxManagerModel(pillarManagerModel);
        IPlayerSettings playerSettings = PlayerFactory.CreatePlayerSettings(mapSettingsResource);
        IPlayerModel playerModel = PlayerFactory.CreatePlayerModel(playerSettings, levelChangeModel);
        
        return new MapWorld2DModel(
            playerModel, 
            pillarManagerModel,
            levelChangeModel,
            parallaxManagerModel
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