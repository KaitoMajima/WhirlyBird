public class MainMenuParallaxManagerModel : IParallaxManagerModel
{
    public float ParallaxOffset => mainMenuSettings.ParallaxBaseValue;
    
    readonly IMainMenuSettings mainMenuSettings;

    public MainMenuParallaxManagerModel (IMainMenuSettings mainMenuSettings)
    {
        this.mainMenuSettings = mainMenuSettings;
    }
    
    public void Initialize () { }
    
    public void Dispose () { }
}