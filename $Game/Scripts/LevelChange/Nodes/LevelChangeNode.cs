using Godot;

public partial class LevelChangeNode : Node2D
{
    [Export] LevelChangeStageNode purpleStageNode;
    [Export] LevelChangeStageNode redStageNode;
    [Export] LevelChangeStageNode finalStageNode;
    
    ILevelChangeModel model;
    
    public void Setup (ILevelChangeModel model)
    {
        this.model = model;
    }
    
    public void Initialize ()
    {
        AddModelListeners();
    }
    
    void SetLevelChange (int id)
    {
        switch (id)
        {
            case 0:
                SetUnchangedState();
                break;
            case 1:
                SetLevelChange1State();
                break;
            case 2:
                SetLevelChange2State();
                break;
            case 3:
                SetLevelChange3State();
                break;
        }
    }
        
    void SetUnchangedState ()
    {
        SetAllObjectsActive(false);
    }
        
    void SetLevelChange1State ()
    {
        purpleStageNode.Visible = true;
        foreach (TweenManager tween in purpleStageNode.StageAnimations)
            tween.PlayTween();
    }

    void SetLevelChange2State ()
    {
        redStageNode.Visible = true;
        foreach (TweenManager tween in redStageNode.StageAnimations)
            tween.PlayTween();
    }
        
    void SetLevelChange3State ()
    {
        finalStageNode.Visible = true;
        foreach (TweenManager tween in finalStageNode.StageAnimations)
            tween.PlayTween();
    }

    void SetAllObjectsActive (bool value)
    {
        purpleStageNode.Visible = value;
        redStageNode.Visible = value;
        finalStageNode.Visible = value;
    }
    
    void HandleLevelChanged ()
    {
        SetLevelChange(model.CurrentLevelId);
    }
    
    void AddModelListeners ()
    {
        model.OnLevelChanged += HandleLevelChanged;
    }

    void RemoveModelListeners ()
    {
        model.OnLevelChanged -= HandleLevelChanged;
    }
    
    public new void Dispose ()
    {
        RemoveModelListeners();
    }
}