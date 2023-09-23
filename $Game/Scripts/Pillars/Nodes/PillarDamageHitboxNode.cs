using System;

public partial class PillarDamageHitboxNode : BaseNode2DHitboxNode, ICollideable
{
    public event Action OnDamageDetected;

    public override MapCollisionType CollisionType => MapCollisionType.Damage;

    public override void NotifyCollision ()
    {
        base.NotifyCollision();
        OnDamageDetected?.Invoke();
    }
}