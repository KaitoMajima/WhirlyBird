using Godot;

public partial class PauseMenuCenterButtons : Node
{
    #region Node Paths
    [Export]
    NodePath ResumeButtonPath { get; set; }
    
    [Export]
    NodePath RetryButtonPath { get; set; }
    
    [Export]
    NodePath MainMenuButtonPath { get; set; }
    
    [Export(PropertyHint.File, "*.tscn")]
    string MainMenuScenePath { get; set; }
    
    [Export]
    NodePath SceneToUnloadPath { get; set; }
    #endregion

    #region Nodes
    Button resumeButton;
    Button retryButton;
    Button mainMenuButton;
    Node sceneToUnload;
    #endregion

    public void Initialize ()
    {
        resumeButton = GetNode<Button>(ResumeButtonPath);
        retryButton = GetNode<Button>(RetryButtonPath);
        mainMenuButton = GetNode<Button>(MainMenuButtonPath);
        sceneToUnload = GetNode<Node>(SceneToUnloadPath);
        AddButtonListeners();
    }

    void AddButtonListeners ()
    {
        resumeButton.Pressed += HandleResumeButtonPressed;
        retryButton.Pressed += HandleRetryButtonPressed;
        mainMenuButton.Pressed += HandleMainMenuButtonPressed;
    }

    void RemoveButtonListeners ()
    {
        resumeButton.Pressed -= HandleResumeButtonPressed;
        retryButton.Pressed -= HandleRetryButtonPressed;        
        mainMenuButton.Pressed -= HandleMainMenuButtonPressed;
    }

    void HandleResumeButtonPressed ()
    {
        //unpause
    }
    
    void HandleRetryButtonPressed ()
    {
        LoadingScope.Instance.Load(sceneToUnload.SceneFilePath, sceneToUnload);
    }
    
    void HandleMainMenuButtonPressed ()
    {
        LoadingScope.Instance.Load(MainMenuScenePath, sceneToUnload);
    }

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}
