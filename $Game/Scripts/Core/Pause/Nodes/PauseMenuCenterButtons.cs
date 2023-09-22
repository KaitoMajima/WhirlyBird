using System;
using Godot;

public partial class PauseMenuCenterButtons : Node
{
    public event Action OnResumeButtonPressed;
    public event Action OnRetryButtonPressed;
    public event Action OnMainMenuButtonPressed;
    
    #region Node Paths
    [Export]
    NodePath ResumeButtonPath { get; set; }
    
    [Export]
    NodePath RetryButtonPath { get; set; }
    
    [Export]
    NodePath MainMenuButtonPath { get; set; }
    
    [Export(PropertyHint.File, "*.tscn")]
    string MainMenuScenePath { get; set; }
    #endregion

    #region Nodes
    Button resumeButton;
    Button retryButton;
    Button mainMenuButton;
    #endregion

    public void Initialize ()
    {
        resumeButton = GetNode<Button>(ResumeButtonPath);
        retryButton = GetNode<Button>(RetryButtonPath);
        mainMenuButton = GetNode<Button>(MainMenuButtonPath);
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
        => OnResumeButtonPressed!();

    void HandleRetryButtonPressed () 
        => OnRetryButtonPressed!();

    void HandleMainMenuButtonPressed () 
        => OnMainMenuButtonPressed!();

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}
