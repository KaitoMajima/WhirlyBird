using Godot;

public partial class MainMenuNode : Node, IMainMenuNode
{
    [Export]
    MainMenuCenterButtons MainMenuCenterButtons { get; set; }

    public void Initialize ()
    {
        MainMenuCenterButtons.Initialize();
    }

    public new void Dispose ()
    {
        MainMenuCenterButtons.Dispose();
        base.Dispose();
    }
}