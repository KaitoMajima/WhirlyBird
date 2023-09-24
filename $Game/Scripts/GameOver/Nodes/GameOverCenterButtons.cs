﻿using System;
using Godot;

public partial class GameOverCenterButtons : Node
{
    public event Action OnRetryButtonPressed;
    public event Action OnMainMenuButtonPressed;
    
    [Export] Button retryButton;
    [Export] Button mainMenuButton;

    public void Initialize ()
    {
        AddButtonListeners();
    }

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