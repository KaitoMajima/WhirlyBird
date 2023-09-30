public class MainMenuModel : IMainMenuModel
{
    public IParallaxManagerModel ParallaxManagerModel { get; }
    
    readonly IMusicManagerSystem musicManagerSystem;

    public MainMenuModel (IParallaxManagerModel parallaxManagerModel, IMusicManagerSystem musicManagerSystem)
    {
        ParallaxManagerModel = parallaxManagerModel;
        this.musicManagerSystem = musicManagerSystem;
    }
    
    public void Initialize ()
    {
        ParallaxManagerModel.Initialize();
        musicManagerSystem.Play(MusicClipType.MainMenu);
    }
}