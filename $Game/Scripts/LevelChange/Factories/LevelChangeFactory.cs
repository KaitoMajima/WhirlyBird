public static class LevelChangeFactory
{
    public static ILevelChangeSettings CreateLevelChangeSettings (MapSettingsResource mapSettingsResource)
        => JsonHelper.DeserializeObjectFromPath<LevelChangeSettings>(mapSettingsResource
            .LevelChangeSettingsJsonPath);
    
    public static ILevelChangeModel CreateLevelChangeModel (
        ILevelChangeSettings levelChangeSettings, 
        IPillarManagerModel pillarManagerModel
    ) => new LevelChangeModel(levelChangeSettings, pillarManagerModel);

    public static ILevelChangeMusicModel CreateLevelChangeMusicModel (
        ILevelChangeModel levelChangeModel, 
        IMusicManagerSystem musicManagerSystem
    ) => new LevelChangeMusicModel(levelChangeModel, musicManagerSystem);
}