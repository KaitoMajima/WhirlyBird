using Godot;

public partial class MainMenuScope : Node
{
    #region Nodes
    [Export] MainMenuNode mainMenuNode;

    public IMainMenuNode MainMenuNode => mainMenuNode;
    #endregion

    #region Scopes
    GameScope GameScope => GameScope.Instance;
    #endregion
    
    public override void _Ready ()
    {
        InitializeNodes();
    }

    public override void _ExitTree ()
    {
        DisposeNodes();
    }
    
    void InitializeNodes ()
    {
        MainMenuNode.Initialize();
    }
    
    void DisposeNodes ()
    {
        MainMenuNode.Dispose();
    }
}