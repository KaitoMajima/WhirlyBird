using Godot;

public partial class MainMenuNode : Node
{
    [Export] 
    public MainMenuUICanvasNode MainMenuUICanvasNode { get; private set; }
    
    [Export]
    public MainMenuWorld2DNode MainMenuWorld2DNode { get; private set; }

    IMainMenuModel mainMenuModel;

    public void Setup (IMainMenuModel mainMenuModel)
    {
        this.mainMenuModel = mainMenuModel;
    }
    
    public void Initialize ()
    {
        MainMenuUICanvasNode.Initialize();
        MainMenuWorld2DNode.Initialize();
    }

    public new void Dispose ()
    {
        MainMenuUICanvasNode.Dispose();
        MainMenuWorld2DNode.Dispose();
        base.Dispose();
    }
}