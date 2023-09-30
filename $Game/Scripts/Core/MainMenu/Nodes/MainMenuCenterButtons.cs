using System;
using Godot;

public partial class MainMenuCenterButtons : Node
{
    public event Action OnPlayButtonPressed;
    public event Action OnCreditsButtonPressed;
    public event Action OnExitButtonPressed;
    
    [Export] Button playButton;
    [Export] Button creditsButton;
    [Export] Button exitButton;
    
    public void Initialize ()
    {
        AddButtonListeners();
    }

    void HandlePlayButtonPressed () 
        => OnPlayButtonPressed?.Invoke();

    void HandleCreditsButtonPressed () 
        => OnCreditsButtonPressed?.Invoke();

    void HandleExitButtonPressed () 
        => OnExitButtonPressed?.Invoke();

    void AddButtonListeners ()
    {
        playButton.Pressed += HandlePlayButtonPressed;
        creditsButton.Pressed += HandleCreditsButtonPressed;
        exitButton.Pressed += HandleExitButtonPressed;
    }
    
    void RemoveButtonListeners ()
    {
        playButton.Pressed -= HandlePlayButtonPressed;
        exitButton.Pressed -= HandleExitButtonPressed;
    }

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}