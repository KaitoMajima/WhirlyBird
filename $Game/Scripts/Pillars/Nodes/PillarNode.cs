using System;
using Godot;

public partial class PillarNode : Node2D, IPillarNode
{
    public event Action OnPillarScoreTriggered;
    public event Action OnPillarDamageTriggered;
    
    [Export] 
    Area2D UpperPillarBody { get; set; }
    
    [Export] 
    Area2D LowerPillarBody { get; set; }

    [Export] 
    Area2D PillarScoreTrigger { get; set; }
    
    public override void _PhysicsProcess (double delta)
    {
        Vector2 speed = new(-500 * (float)delta, 0);
        Position += speed;
    }

    public void Initialize ()
    {
        AddTriggerListeners();
    }

    public void SetPosition (Vector2 position)
    {
        Position = position;
    }
    
    void TriggerScore (Node2D body)
    {
        if (body is not ICollideable)
            return;
            
        OnPillarScoreTriggered?.Invoke();
    }

    void TriggerDamage (Node body)
    {
        if (body is not ICollideable)
            return;
        
        OnPillarDamageTriggered?.Invoke();
    }

    void HandleScoreEntered (Node2D body) 
        => TriggerScore(body);

    void HandleUpperPillarEntered (Node body) 
        => TriggerDamage(body);

    void HandleLowerPillarEntered (Node body) 
        => TriggerDamage(body);

    void AddTriggerListeners ()
    {
        PillarScoreTrigger.BodyEntered += HandleScoreEntered;
        UpperPillarBody.BodyEntered += HandleUpperPillarEntered;
        LowerPillarBody.BodyEntered += HandleLowerPillarEntered;
    }

    void RemoveTriggerListeners ()
    {
        PillarScoreTrigger.BodyEntered -= HandleScoreEntered;
        UpperPillarBody.BodyEntered -= HandleUpperPillarEntered;
        LowerPillarBody.BodyEntered -= HandleLowerPillarEntered;
    }

    public new void Dispose ()
    {
        RemoveTriggerListeners();
        base.Dispose();
    }
}