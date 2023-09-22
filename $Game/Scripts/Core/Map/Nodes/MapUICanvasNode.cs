using Godot;

public partial class MapUICanvasNode : Node, IMapUICanvasNode
{
    [Export] 
    NodePath PauseNodePath { get; set; }
    
    [Export]
    NodePath PauseButtonPath { get; set; }

    public IPauseNode PauseNode { get; private set; }

    IPauseModel pauseModel;
    Button pauseButton;
    
    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }
    
    public void Initialize ()
    {
        PauseNode = GetNode<IPauseNode>(PauseNodePath);
        PauseNode.Setup(pauseModel);
        PauseNode.Initialize();

        pauseButton = GetNode<Button>(PauseButtonPath);

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