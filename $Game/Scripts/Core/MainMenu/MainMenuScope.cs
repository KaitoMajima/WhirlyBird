using Godot;

public partial class MainMenuScope : Node
{
    [Export]
    public MainMenuNode MainMenuNode { get; private set; }
    
    GameScope GameScope => GameScope.Instance;
    
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