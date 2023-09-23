using Godot;

public partial class MapUICanvasNode : Node, IMapUICanvasNode
{
    [Export]
    Button PauseButton { get; set; }
    
    [Export]
    PauseNode PauseNodeInstance { get; set; }
    
    public IPauseNode PauseNode { get; private set; }

    IPauseModel pauseModel;
    
    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }
    
    public void Initialize ()
    {
        PauseNode = PauseNodeInstance;
        PauseNode.Setup(pauseModel);
        PauseNode.Initialize();
        
        AddButtonListeners();
    }

    void AddButtonListeners ()
    {
        PauseButton.Pressed += HandlePauseButtonPressed;
    }
    
    void RemoveButtonListeners ()
    {
        PauseButton.Pressed -= HandlePauseButtonPressed;
    }

    void HandlePauseButtonPressed ()
    {
        pauseModel.SetPause(true);
    }

    public new void Dispose ()
    {
        RemoveButtonListeners();
        base.Dispose();
    }
}