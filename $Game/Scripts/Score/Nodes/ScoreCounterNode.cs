using Godot;

public partial class ScoreCounterNode : Node
{
    [Export] Label scoreLabel;
    [Export] TextScaleUITween scoringScaleBackAnimation;
    [Export] RotateUITween scoringRotateBackAnimation;
    
    IScoreCounterModel model;
    
    public void Setup (IScoreCounterModel model)
    {
        this.model = model; 
    }

    public void Initialize ()
    {
        AddModelListeners();
        SetAllScoreText();
    }
    
    void SetAllScoreText ()
    {
        scoreLabel.Text = model.Score.ToString();
    }
    
    void HandleScoreDetected ()
    {
        SetAllScoreText();
        scoringScaleBackAnimation.PlayTween();
        scoringRotateBackAnimation.PlayTween();
    }

    void AddModelListeners ()
    {
        model.OnScoreDetected += HandleScoreDetected;
    }

    void RemoveModelListeners ()
    {
        model.OnScoreDetected -= HandleScoreDetected;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}