using Godot;

public partial class ScoreCounterNode : Node
{
    [Export] Label scoreLabel;
    [Export] Label highscoreLabel;
    [Export] TextScaleUITween scoringScaleBackAnimation;
    [Export] RotateUITween scoringRotateBackAnimation;
    [Export] SoundEffectNode scoreSFX;
    
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
        highscoreLabel.Text = $"Highscore: {model.Highscore}";
    }
    
    void HandleScoreDetected ()
    {
        SetAllScoreText();
        scoringScaleBackAnimation.PlayTween();
        scoringRotateBackAnimation.PlayTween();
        scoreSFX.Play();
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