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
        CreateNodes();
        ProcessNodesInitialization();
    }

    public override void _ExitTree ()
    {
        ProcessNodesDisposal();
    }

    void CreateNodes ()
    {
        MainMenuNode = MainMenuNodeFactory.CreateMainMenuNode(
            this,
            mainMenuNodePath
        );
    }
    
    void ProcessNodesInitialization ()
    {
        MainMenuNode.Initialize();
    }
    
    void ProcessNodesDisposal ()
    {
        MainMenuNode.Dispose();
    }
}