using Godot;

public partial class MapUICanvasNode : Node
{
    [Export]
    public PauseNode PauseNode { get; private set; }
    
    [Export] Button pauseButton;

    IPauseModel pauseModel;
    
    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }
    
    public void Initialize ()
    {
        PauseNode.Setup(pauseModel);
        PauseNode.Initialize();
        
        AddButtonListeners();
    }

    void AddButtonListeners ()
    {
        pauseButton.Pressed += HandlePauseButtonPressed;
    }
    
    void RemoveButtonListeners ()
    {
        pauseButton.Pressed -= HandlePauseButtonPressed;
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