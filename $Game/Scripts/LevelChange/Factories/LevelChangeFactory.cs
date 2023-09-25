public static class LevelChangeFactory
{
    public static ILevelChangeSettings CreateLevelChangeSettings (MapSettingsResource mapSettingsResource)
        => JsonHelper.DeserializeObjectFromPath<LevelChangeSettings>(mapSettingsResource
            .LevelChangeSettingsJsonPath);
    
    public static ILevelChangeModel CreateLevelChangeModel (
        ILevelChangeSettings levelChangeSettings, 
        IPillarManagerModel pillarManagerModel,
        IMusicManagerModel musicManagerModel
    ) => new LevelChangeModel(levelChangeSettings, pillarManagerModel, musicManagerModel);
}