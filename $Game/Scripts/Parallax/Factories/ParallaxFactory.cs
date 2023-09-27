public static class ParallaxFactory
{
    public static IParallaxManagerModel CreateMapParallaxManagerModel (IPillarManagerModel pillarManagerModel) 
        => new MapParallaxManagerModel(pillarManagerModel);
    
    public static IParallaxManagerModel CreateMainMenuParallaxManagerModel (IMainMenuSettings mainMenuSettings) 
        => new MainMenuParallaxManagerModel(mainMenuSettings);
}