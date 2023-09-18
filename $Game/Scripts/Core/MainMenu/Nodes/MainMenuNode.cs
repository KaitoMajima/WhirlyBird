using Godot;

public partial class MainMenuNode : Node, IMainMenuNode
{
    #region Node Paths
    [Export]
    NodePath MainMenuCenterButtonsPath { get; set; }
    #endregion

    #region Nodes
    MainMenuCenterButtons mainMenuCenterButtons;
    #endregion

    public void Initialize ()
    {
        mainMenuCenterButtons = GetNode<MainMenuCenterButtons>(MainMenuCenterButtonsPath);
        mainMenuCenterButtons.Initialize();
    }

    public new void Dispose ()
    {
        mainMenuCenterButtons.Dispose();
        base.Dispose();
    }
}