using Godot;

public partial class MainMenuScope : Node
{
    public IMainMenuModel MainMenuModel { get; private set; }
    
    [Export]
    public MainMenuNode MainMenuNode { get; private set; }
    
    [Export]
    public MainMenuSettingsResource MainMenuSettingsResource { get; private set; }
    
    GameScope GameScope => GameScope.Instance;
    
    public override void _Ready ()
    {
        SetupModels();
        SetupNodes();
        InitializeModels();
        InitializeNodes();
    }

    public override void _ExitTree ()
    {
        DisposeNodes();
    }
    
    void SetupModels ()
    {
        MainMenuModel = MainMenuFactory.CreateMainMenuModel(
            MainMenuSettingsResource, 
            GameScope.GameModel.MusicManagerModel
        );
    }
    
    void SetupNodes ()
    {
        MainMenuFactory.SetupMainMenuNode(MainMenuNode, MainMenuModel);
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