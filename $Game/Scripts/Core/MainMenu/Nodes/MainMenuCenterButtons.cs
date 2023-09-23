using Godot;

public partial class MainMenuCenterButtons : Node
{
    [Export(PropertyHint.File, "*.tscn")] 
    string playButtonScenePath;
    
    [Export] Button playButton;
    [Export] Button exitButton;
    [Export] Node sceneToUnload;
    
    public void Initialize ()
    {
        AddButtonListeners();
    }

    void AddButtonListeners ()
    {
        playButton.Pressed += HandlePlayButtonPressed;
        exitButton.Pressed += HandleExitButtonPressed;
    }

    void RemoveButtonListeners ()
    {
        playButton.Pressed -= HandlePlayButtonPressed;
        exitButton.Pressed -= HandleExitButtonPressed;
    }

    void HandlePlayButtonPressed ()
    {
        LoadingScope.Instance.Load(playButtonScenePath, sceneToUnload);
    }

    void HandleExitButtonPressed ()
    {
        GetTree().Quit();
    }

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}