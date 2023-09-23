using System;
using Godot;

public partial class PillarNode : Node2D
{
    public event Action<PillarNode> OnPillarMarkedForDestruction;
    
    [Export] PillarDamageHitboxNode upperPillarDamageHitbox;
    [Export] PillarDamageHitboxNode lowerPillarDamageHitbox;
    [Export] PillarScoreHitboxNode pillarScoreHitbox;
    [Export] Timer pillarSpawnTimer;

    IPillarModel pillarModel;
    
    public override void _PhysicsProcess (double delta)
    {
        Vector2 speed = new(-500 * (float)delta, 0);
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
    }

    public void SetPosition (Vector2 position)
    {
        Position = position;
    }
    
    void ResetPillarHitboxes ()
    {
        upperPillarDamageHitbox.Reset();
        lowerPillarDamageHitbox.Reset();
        pillarScoreHitbox.Reset();
    }
    
    void TriggerScore () 
        => GD.Print("Pillar has detected a score!");

    void TriggerDamage () 
        => GD.Print("Pillar has damaged!");

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