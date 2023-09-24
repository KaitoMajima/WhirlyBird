using Godot;

public partial class ScoreCounterNode : Node
{
    [Export] Label scoreLabel;
    [Export] TweenManager scoringAnimation;
    
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
        scoringAnimation.PlayTween();
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