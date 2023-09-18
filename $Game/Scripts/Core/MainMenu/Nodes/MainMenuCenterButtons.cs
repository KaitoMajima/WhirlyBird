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
    
    [Export]
    NodePath SceneToUnloadPath { get; set; }
    #endregion

    #region Nodes
    Button playButton;
    Button exitButton;
    Node sceneToUnload;
    #endregion

    public void Initialize ()
    {
        playButton = GetNode<Button>(PlayButtonPath);
        exitButton = GetNode<Button>(ExitButtonPath);
        sceneToUnload = GetNode<Node>(SceneToUnloadPath);
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
        LoadingScope.Instance.Load(PlayButtonScene, sceneToUnload);
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