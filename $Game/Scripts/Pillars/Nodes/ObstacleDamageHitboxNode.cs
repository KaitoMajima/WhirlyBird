using System;

public partial class ObstacleDamageHitboxNode : BaseArea2DHitboxNode, ICollideable
{
    public event Action OnDamageDetected;

    public override MapCollisionType CollisionType => MapCollisionType.Damage;

    public override void NotifyCollision ()
    {
        base.NotifyCollision();
        OnDamageDetected?.Invoke();
    }
}