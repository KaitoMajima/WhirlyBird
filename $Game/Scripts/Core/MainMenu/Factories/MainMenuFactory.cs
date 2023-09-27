public class MainMenuFactory
{
    public static IMainMenuModel CreateMainMenuModel (
        MainMenuSettingsResource mainMenuSettingsResource, 
        IMusicManagerModel musicManagerModel
    )
    {
        IMainMenuSettings mainMenuSettings = CreateMainMenuSettings(mainMenuSettingsResource);
        IParallaxManagerModel parallaxManagerModel = ParallaxFactory.CreateMainMenuParallaxManagerModel(mainMenuSettings);
        return new MainMenuModel(parallaxManagerModel, musicManagerModel);
    }

    public static void SetupMainMenuNode (
        MainMenuNode mainMenuNode, 
        IMainMenuModel mainMenuModel
    ) => mainMenuNode.Setup(mainMenuModel.ParallaxManagerModel);
    
    static IMainMenuSettings CreateMainMenuSettings (MainMenuSettingsResource mainMenuSettingsResource)
        => JsonHelper.DeserializeObjectFromPath<MainMenuSettings>(mainMenuSettingsResource.MainMenuSettingsJsonPath);
}