public static class MapFactory
{
    public static IMapModel CreateMapModel (
        IGameSaveData saveData,
        IMainGameSavingSystem saveSystem,
        ITimeProvider timeProvider, 
        IRandomProvider randomProvider,
        IMusicManagerSystem musicManagerSystem,
        MapSettingsResource mapSettingsResource,
        IGameStateProvider gameStateProvider
    )
    {
        IMapWorld2DModel mapWorld2DModel = CreateMapWorld2DModel(randomProvider, mapSettingsResource, musicManagerSystem);
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
            mapInputDetectionModel
        );
    }
    
    public static void SetupMapNode (
        MapNode mapNode, 
        IMapModel mapModel,
        IRandomProvider randomProvider
    )
    {
        SetupMapInputDetectionNode(
            mapNode.MapInputDetectionNode, 
            mapModel.MapInputDetectionModel
        );
        SetupMapWorld2DNode(
            mapNode.MapWorld2DNode, 
            mapModel.MapWorld2DModel, 
            mapModel.MapInputDetectionModel, 
            randomProvider
        );
        SetupMapUICanvasNode(
            mapNode.MapUICanvasNode, 
            mapModel.MapUICanvasModel
        );
        mapNode.Setup(mapModel);
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
        IMusicManagerSystem musicManagerSystem
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
            pillarManagerModel
        );
        ILevelChangeMusicModel levelChangeMusicModel = LevelChangeFactory.CreateLevelChangeMusicModel(
            levelChangeModel, 
            musicManagerSystem
        );
        IParallaxManagerModel parallaxManagerModel =
            ParallaxFactory.CreateMapParallaxManagerModel(pillarManagerModel);
        IPlayerSettings playerSettings = PlayerFactory.CreatePlayerSettings(mapSettingsResource);
        IPlayerModel playerModel = PlayerFactory.CreatePlayerModel(playerSettings, levelChangeModel);
        
        return new MapWorld2DModel(
            playerModel, 
            pillarManagerModel,
            levelChangeModel,
            parallaxManagerModel,
            levelChangeMusicModel
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

    static void SetupMapWorld2DNode (
        MapWorld2DNode mapWorld2DNode,
        IMapWorld2DModel mapWorld2DModel, 
        IMapInputDetectionModel mapInputDetectionModel,
        IRandomProvider randomProvider
    )
    {
        mapWorld2DNode.Setup(
            mapWorld2DModel.PlayerModel, 
            mapInputDetectionModel, 
            mapWorld2DModel.PillarManagerModel, 
            randomProvider, 
            mapWorld2DModel.LevelChangeModel, 
            mapWorld2DModel.ParallaxManagerModel
        );
    }
    
    static void SetupMapUICanvasNode (
        MapUICanvasNode mapUICanvasNode,
        IMapUICanvasModel mapUICanvasModel
    )
    {
        mapUICanvasNode.Setup(
            mapUICanvasModel.PauseModel,
            mapUICanvasModel.ScoreCounterModel,
            mapUICanvasModel.GameOverModel
        );
    }
    
    static void SetupMapInputDetectionNode (
        MapInputDetectionNode mapInputDetectionNode,
        IMapInputDetectionModel mapInputDetectionModel
    )
    {
        mapInputDetectionNode.Setup(mapInputDetectionModel);
    }
}