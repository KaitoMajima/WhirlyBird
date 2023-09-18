using Godot;

public partial class MainMenuCenterButtons : Node
{
    #region Node Paths
    [Export]
    NodePath PlayButtonPath { get; set; }
    
    [Export]
    NodePath ExitButtonPath { get; set; }
    
    [Export]
    PackedScene PlayButtonScene { get; set; }
    #endregion

    #region Nodes
    Button playButton;
    Button exitButton;
    #endregion

    public void Initialize ()
    {
        playButton = GetNode<Button>(PlayButtonPath);
        exitButton = GetNode<Button>(ExitButtonPath);
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
        LoadingScope.Instance.Load(PlayButtonScene);
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