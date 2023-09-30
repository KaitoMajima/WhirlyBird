public class MainMenuFactory
{
    public static IMainMenuModel CreateMainMenuModel (
        MainMenuSettingsResource mainMenuSettingsResource, 
        IMusicManagerSystem musicManagerSystem
    )
    {
        IMainMenuSettings mainMenuSettings = CreateMainMenuSettings(mainMenuSettingsResource);

        IMainMenuWorld2DModel mainMenuWorld2DModel = CreateMainMenuWorld2DModel(mainMenuSettings);
        IMainMenuUICanvasModel mainMenuUICanvasModel = CreateMainMenuUICanvasModel();
        
        return new MainMenuModel(mainMenuWorld2DModel, mainMenuUICanvasModel, musicManagerSystem);
    }

    public static void SetupMainMenuNode (
        MainMenuNode mainMenuNode, 
        IMainMenuModel mainMenuModel
    )
    {
        SetupMainMenuWorld2DNode(
            mainMenuNode.MainMenuWorld2DNode, 
            mainMenuModel.MainMenuWorld2DModel.ParallaxManagerModel
        );
        SetupMainMenuUICanvasNode(mainMenuNode.MainMenuUICanvasNode);
        mainMenuNode.Setup(mainMenuModel);
    }

    static IMainMenuSettings CreateMainMenuSettings (MainMenuSettingsResource mainMenuSettingsResource)
        => JsonHelper.DeserializeObjectFromPath<MainMenuSettings>(mainMenuSettingsResource.MainMenuSettingsJsonPath);

    static IMainMenuWorld2DModel CreateMainMenuWorld2DModel (IMainMenuSettings mainMenuSettings)
    {
        IParallaxManagerModel parallaxManagerModel = ParallaxFactory.CreateMainMenuParallaxManagerModel(mainMenuSettings);
        return new MainMenuWorld2DModel(parallaxManagerModel);
    }
        
    static IMainMenuUICanvasModel CreateMainMenuUICanvasModel ()
    {
        return new MainMenuUICanvasModel();
    }
    
    static void SetupMainMenuWorld2DNode (MainMenuWorld2DNode mainMenuWorld2DNode, IParallaxManagerModel parallaxManagerModel)
    {
        mainMenuWorld2DNode.Setup(parallaxManagerModel);
    }

    static void SetupMainMenuUICanvasNode (MainMenuUICanvasNode mainMenuUICanvasNode)
    {
        
    }
}