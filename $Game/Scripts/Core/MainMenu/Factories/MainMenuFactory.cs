public class MainMenuFactory
{
    public static IMainMenuModel CreateMainMenuModel (
        MainMenuSettingsResource mainMenuSettingsResource, 
        IMusicManagerSystem musicManagerSystem
    )
    {
        IMainMenuSettings mainMenuSettings = CreateMainMenuSettings(mainMenuSettingsResource);
        IParallaxManagerModel parallaxManagerModel = ParallaxFactory.CreateMainMenuParallaxManagerModel(mainMenuSettings);
        return new MainMenuModel(parallaxManagerModel, musicManagerSystem);
    }

    public static void SetupMainMenuNode (
        MainMenuNode mainMenuNode, 
        IMainMenuModel mainMenuModel
    ) => mainMenuNode.Setup(mainMenuModel.ParallaxManagerModel);
    
    static IMainMenuSettings CreateMainMenuSettings (MainMenuSettingsResource mainMenuSettingsResource)
        => JsonHelper.DeserializeObjectFromPath<MainMenuSettings>(mainMenuSettingsResource.MainMenuSettingsJsonPath);
}