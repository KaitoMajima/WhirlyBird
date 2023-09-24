using Godot;

public partial class MapUICanvasNode : Node
{
    [Export]
    public PauseNode PauseNode { get; private set; }
    
    [Export]
    public ScoreCounterNode ScoreCounterNode { get; private set; }
    
    [Export]
    public GameOverNode GameOverNode { get; private set; }
    
    [Export] Button pauseButton;

    IPauseModel pauseModel;
    
    public void Setup (
        IPauseModel pauseModel, 
        IScoreCounterModel scoreCounterModel,
        IGameOverModel gameOverModel
    )
    {
        this.pauseModel = pauseModel;
        PauseNode.Setup(pauseModel);
        ScoreCounterNode.Setup(scoreCounterModel);
        GameOverNode.Setup(gameOverModel);
    }
    
    public void Initialize ()
    {
        PauseNode.Initialize();
        ScoreCounterNode.Initialize();
        GameOverNode.Initialize();
        
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
        PauseNode.Dispose();
        ScoreCounterNode.Dispose();
        GameOverNode.Dispose();
        base.Dispose();
    }
}