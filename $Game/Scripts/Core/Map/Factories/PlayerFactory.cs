public static class PlayerFactory
{
    public static IPlayerSettings CreatePlayerSettings (MapSettingsResource mapSettingsResource) 
        => JsonHelper.DeserializeObjectFromPath<PlayerSettings>(mapSettingsResource.PlayerSettingsJsonPath);
    
    public static IPlayerModel CreatePlayerModel (IPlayerSettings playerSettings) 
        => new PlayerModel(playerSettings);
}