public static class PillarFactory
{
    public static IPillarSpawnSettings CreatePillarSpawnSettings (MapSettingsResource mapSettingsResource)
        => JsonHelper.DeserializeObjectFromPath<PillarSpawnSettings>(mapSettingsResource.PillarSpawnSettingsJsonPath);
    
    public static IPillarManagerModel CreatePillarManagerModel (
        IPillarSpawnSettings pillarSpawnSettings, 
        IRandomProvider randomProvider
    ) => new PillarManagerModel(pillarSpawnSettings, randomProvider);

    public static IPillarModel CreatePillarModel () 
        => new PillarModel();
}