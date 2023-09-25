using Godot;

public partial class MainMenuScope : Node
{
    public IMainMenuModel MainMenuModel { get; private set; }
    
    [Export]
    public MainMenuNode MainMenuNode { get; private set; }
    
    GameScope GameScope => GameScope.Instance;
    
    public override void _Ready ()
    {
        SetupModels();
        InitializeModels();
        InitializeNodes();
    }

    public override void _ExitTree ()
    {
        DisposeNodes();
    }
    
    void SetupModels ()
    {
        MainMenuModel = MainMenuFactory.CreateMainMenuModel(GameScope.GameModel.MusicManagerModel);
    }
    
    void InitializeModels ()
    {
        MainMenuModel.Initialize();
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