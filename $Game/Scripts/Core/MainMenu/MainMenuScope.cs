using Godot;

public partial class MainMenuScope : Node
{
    #region NodePaths
    [Export] NodePath mainMenuNodePath;
    #endregion

    #region Nodes
    public IMainMenuNode MainMenuNode { get; private set; }
    #endregion

    #region Scopes
    GameScope GameScope => GameScope.Instance;
    #endregion
    
    public override void _Ready ()
    {
        MainMenuNode = GetNode<MainMenuNode>(mainMenuNodePath);
    }
}