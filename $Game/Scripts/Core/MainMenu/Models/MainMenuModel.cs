public class MainMenuModel : IMainMenuModel
{
    public IMainMenuWorld2DModel MainMenuWorld2DModel { get; }
    public IMainMenuUICanvasModel MainMenuUICanvasModel { get; }

    readonly IMusicManagerSystem musicManagerSystem;

    public MainMenuModel (
        IMainMenuWorld2DModel mainMenuWorld2DModel, 
        IMainMenuUICanvasModel mainMenuUICanvasModel, 
        IMusicManagerSystem musicManagerSystem
    )
    {
        MainMenuWorld2DModel = mainMenuWorld2DModel;
        MainMenuUICanvasModel = mainMenuUICanvasModel;
        this.musicManagerSystem = musicManagerSystem;
    }
    
    public void Initialize ()
    {
        MainMenuWorld2DModel.Initialize();
        MainMenuUICanvasModel.Initialize();
        musicManagerSystem.Play(MusicClipType.MainMenu);
    }

    public void Dispose ()
    {
        MainMenuWorld2DModel.Dispose();
        MainMenuUICanvasModel.Dispose();
    }
}