using Godot;

public partial class MainMenuNode : Node
{
    [Export] MainMenuCenterButtons mainMenuCenterButtons;

    public void Initialize ()
    {
        mainMenuCenterButtons.Initialize();
    }

    public new void Dispose ()
    {
        mainMenuCenterButtons.Dispose();
        base.Dispose();
    }
}