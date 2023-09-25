public class MainMenuModel : IMainMenuModel
{
    readonly IMusicManagerModel musicManagerModel;

    public MainMenuModel (IMusicManagerModel musicManagerModel)
    {
        this.musicManagerModel = musicManagerModel;
    }
    
    public void Initialize ()
    {
        musicManagerModel.Play(MusicClipType.MainMenu);
    }
}