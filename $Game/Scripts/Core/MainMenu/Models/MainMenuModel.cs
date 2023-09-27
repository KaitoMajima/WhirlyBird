public class MainMenuModel : IMainMenuModel
{
    public IParallaxManagerModel ParallaxManagerModel { get; }
    
    readonly IMusicManagerModel musicManagerModel;

    public MainMenuModel (IParallaxManagerModel parallaxManagerModel, IMusicManagerModel musicManagerModel)
    {
        ParallaxManagerModel = parallaxManagerModel;
        this.musicManagerModel = musicManagerModel;
    }
    
    public void Initialize ()
    {
        ParallaxManagerModel.Initialize();
        musicManagerModel.Play(MusicClipType.MainMenu);
    }
}