using Godot;
using Godot.Sharp.Extras;

public partial class MainMenuScope : Node
{
    [NodePath] 
    public MainMenuNode _mainMenuNode;
    
    public override void _Ready ()
    {
        this.OnReady();
        GD.Print("Is Main Menu Node null?", _mainMenuNode == null);
    }
}