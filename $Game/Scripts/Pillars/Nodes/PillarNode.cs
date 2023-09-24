using System;
using Godot;

public partial class PillarNode : Node2D
{
    public event Action<PillarNode> OnPillarMarkedForDestruction;

    [Export] Move2DTween upperPillarMoveInAnimation;
    [Export] Move2DTween upperPillarMoveAwayAnimation;
    [Export] Move2DTween lowerPillarMoveInAnimation;
    [Export] Move2DTween lowerPillarMoveAwayAnimation;
    [Export] PillarDamageHitboxNode upperPillarDamageHitbox;
    [Export] PillarDamageHitboxNode lowerPillarDamageHitbox;
    [Export] PillarScoreHitboxNode pillarScoreHitbox;
    [Export] Timer pillarSpawnTimer;

    IPillarModel pillarModel;
    float moveSpeed;
    
    public override void _PhysicsProcess (double delta)
    {
        Vector2 speed = new(-moveSpeed * (float)delta, 0);
        Position += speed;
    }

    public void Setup (IPillarModel pillarModel)
    {
        this.pillarModel = pillarModel;
    }

    public void Initialize ()
    {
        RemoveTriggerListeners();
        AddModelListeners();
        AddTriggerListeners();
        
        ResetPillarHitboxes();
        pillarModel.StartTimedDestruction(pillarSpawnTimer);
        upperPillarMoveInAnimation.PlayTween();
        lowerPillarMoveInAnimation.PlayTween();
    }

    public void SetPosition (Vector2 position)
    {
        Position = position;
    }

    public void SetSpeed (float speed)
    {
        moveSpeed = speed;
    }
    
    void ResetPillarHitboxes ()
    {
        upperPillarDamageHitbox.Reset();
        lowerPillarDamageHitbox.Reset();
        pillarScoreHitbox.Reset();
    }

    void TriggerScore ()
    {
        upperPillarMoveAwayAnimation.PlayTween();
        lowerPillarMoveAwayAnimation.PlayTween();
    }

    void TriggerDamage () { }

    void HandlePillarMarkedForDestruction () 
        => OnPillarMarkedForDestruction?.Invoke(this);

    void HandleScoreDetected () 
        => TriggerScore();

    void HandleLowerPillarDamageDetected () 
        => TriggerDamage();

    void HandleUpperPillarDamageDetected () 
        => TriggerDamage();

    void AddModelListeners ()
    {
        pillarModel.OnPillarMarkedForDestruction += HandlePillarMarkedForDestruction;
    }
    
    void AddTriggerListeners ()
    {
        pillarScoreHitbox.OnScoreDetected += HandleScoreDetected;
        upperPillarDamageHitbox.OnDamageDetected += HandleUpperPillarDamageDetected;
        lowerPillarDamageHitbox.OnDamageDetected += HandleLowerPillarDamageDetected;
    }
    
    void RemoveModelListeners ()
    {
        pillarModel.OnPillarMarkedForDestruction -= HandlePillarMarkedForDestruction;
    }
    
    void RemoveTriggerListeners ()
    {
        pillarScoreHitbox.OnScoreDetected -= HandleScoreDetected;
        upperPillarDamageHitbox.OnDamageDetected -= HandleUpperPillarDamageDetected;
        lowerPillarDamageHitbox.OnDamageDetected -= HandleLowerPillarDamageDetected;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        RemoveTriggerListeners();
        base.Dispose();
    }
}