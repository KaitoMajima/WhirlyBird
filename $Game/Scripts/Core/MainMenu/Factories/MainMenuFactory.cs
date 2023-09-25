public class MainMenuFactory
{
    public static IMainMenuModel CreateMainMenuModel (IMusicManagerModel musicManagerModel) 
        => new MainMenuModel(musicManagerModel);
}