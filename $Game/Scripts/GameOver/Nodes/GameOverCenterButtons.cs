using System;
using Godot;

public partial class GameOverCenterButtons : Control
{
    public event Action OnRetryButtonPressed;
    public event Action OnMainMenuButtonPressed;
    
    [Export] Button retryButton;
    [Export] Button mainMenuButton;

    public void Initialize ()
    {
        AddButtonListeners();
    }

    public void SetMouseFilter (MouseFilterEnum filter)
    {
        retryButton.MouseFilter = filter;
        mainMenuButton.MouseFilter = filter;
    }
    
    void HandleRetryButtonPressed () 
        => OnRetryButtonPressed!();

    void HandleMainMenuButtonPressed () 
        => OnMainMenuButtonPressed!();
    
    void AddButtonListeners ()
    {
        retryButton.Pressed += HandleRetryButtonPressed;
        mainMenuButton.Pressed += HandleMainMenuButtonPressed;
    }

    void RemoveButtonListeners ()
    {
        retryButton.Pressed -= HandleRetryButtonPressed;        
        mainMenuButton.Pressed -= HandleMainMenuButtonPressed;
    }

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}