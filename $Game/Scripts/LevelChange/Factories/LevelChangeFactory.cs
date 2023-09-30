public static class LevelChangeFactory
{
    public static ILevelChangeSettings CreateLevelChangeSettings (MapSettingsResource mapSettingsResource)
        => JsonHelper.DeserializeObjectFromPath<LevelChangeSettings>(mapSettingsResource
            .LevelChangeSettingsJsonPath);
    
    public static ILevelChangeModel CreateLevelChangeModel (
        ILevelChangeSettings levelChangeSettings, 
        IPillarManagerModel pillarManagerModel,
        IMusicManagerSystem musicManagerSystem
    ) => new LevelChangeModel(levelChangeSettings, pillarManagerModel, musicManagerSystem);
}