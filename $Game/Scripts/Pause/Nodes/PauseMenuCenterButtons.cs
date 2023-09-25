using System;
using Godot;

public partial class PauseMenuCenterButtons : Node
{
    public event Action OnResumeButtonPressed;
    public event Action OnRetryButtonPressed;
    public event Action OnMainMenuButtonPressed;
    
    [Export] Button resumeButton;
    [Export] Button retryButton;
    [Export] Button mainMenuButton;

    public void Initialize ()
    {
        AddButtonListeners();
    }

    void HandleResumeButtonPressed () 
        => OnResumeButtonPressed!();

    void HandleRetryButtonPressed () 
        => OnRetryButtonPressed!();

    void HandleMainMenuButtonPressed () 
        => OnMainMenuButtonPressed!();
    
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

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}
