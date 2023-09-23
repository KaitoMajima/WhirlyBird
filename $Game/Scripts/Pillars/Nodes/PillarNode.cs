using Godot;

public partial class PillarNode : Node2D, IPillarNode
{
    [Export] 
    PillarDamageHitboxNode UpperPillarDamageHitbox { get; set; }
    
    [Export] 
    PillarDamageHitboxNode LowerPillarDamageHitbox { get; set; }

    [Export] 
    PillarScoreHitboxNode PillarScoreHitbox { get; set; }
    
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
    
    void TriggerScore () 
        => GD.Print("Pillar has detected a score!");

    void TriggerDamage () 
        => GD.Print("Pillar has damaged!");

    void HandleScoreDetected () 
        => TriggerScore();

    void HandleLowerPillarDamageDetected () 
        => TriggerDamage();

    void HandleUpperPillarDamageDetected () 
        => TriggerDamage();

    void AddTriggerListeners ()
    {
        PillarScoreHitbox.OnScoreDetected += HandleScoreDetected;
        UpperPillarDamageHitbox.OnDamageDetected += HandleUpperPillarDamageDetected;
        LowerPillarDamageHitbox.OnDamageDetected += HandleLowerPillarDamageDetected;
    }
    
    void RemoveTriggerListeners ()
    {
        PillarScoreHitbox.OnScoreDetected -= HandleScoreDetected;
        UpperPillarDamageHitbox.OnDamageDetected -= HandleUpperPillarDamageDetected;
        LowerPillarDamageHitbox.OnDamageDetected -= HandleLowerPillarDamageDetected;
    }

    public new void Dispose ()
    {
        RemoveTriggerListeners();
        base.Dispose();
    }
}