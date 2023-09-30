using System;
using Godot;

public partial class CreditsCenterButtons : Control
{
    public event Action OnBackButtonPressed;

    [Export] Button backButton;
    
    public void Initialize ()
    {
        AddButtonListeners();
    }

    void HandleButtonPressed () 
        => OnBackButtonPressed?.Invoke();

    void AddButtonListeners ()
    {
        backButton.Pressed += HandleButtonPressed;
    }

    void RemoveButtonListeners ()
    {
        backButton.Pressed -= HandleButtonPressed;
    }

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}