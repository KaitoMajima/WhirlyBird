using Godot;

public partial class MainMenuUICanvasNode : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")] 
    string mapScopeScenePath;

    [Export] Control mainScreen;
    [Export] Control creditsScreen;
    [Export] MainMenuCenterButtons mainMenuCenterButtons;
    [Export] CreditsCenterButtons creditsCenterButtons;
    [Export] Node sceneToUnload;
    
    public void Initialize ()
    {
        mainMenuCenterButtons.Initialize();
        creditsCenterButtons.Initialize();
        AddNodeListeners();
    }
    
    void HandlePlayButtonPressed ()
    {
        LoadingScope.Instance.Load(mapScopeScenePath, sceneToUnload);
    }
    
    void HandleCreditsButtonPressed ()
    {
        mainScreen.Visible = false;
        creditsScreen.Visible = true;
    }
    
    void HandleExitButtonPressed ()
    {
        GetTree().Quit();
    }
    
    void HandleCreditsBackButtonPressed ()
    {
        mainScreen.Visible = true;
        creditsScreen.Visible = false;
    }

    void AddNodeListeners ()
    {
        mainMenuCenterButtons.OnPlayButtonPressed += HandlePlayButtonPressed;
        mainMenuCenterButtons.OnCreditsButtonPressed += HandleCreditsButtonPressed;
        mainMenuCenterButtons.OnExitButtonPressed += HandleExitButtonPressed;
        creditsCenterButtons.OnBackButtonPressed += HandleCreditsBackButtonPressed;
    }

    void RemoveNodeListeners ()
    {
        mainMenuCenterButtons.OnPlayButtonPressed -= HandlePlayButtonPressed;
        mainMenuCenterButtons.OnCreditsButtonPressed -= HandleCreditsButtonPressed;
        mainMenuCenterButtons.OnExitButtonPressed -= HandleExitButtonPressed;
        creditsCenterButtons.OnBackButtonPressed -= HandleCreditsBackButtonPressed;
    }

    public new void Dispose ()
    {
        RemoveNodeListeners();
        mainMenuCenterButtons.Dispose();
        base.Dispose();
    }
}