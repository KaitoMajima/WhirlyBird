using System;

public partial class PillarScoreHitboxNode : BaseArea2DHitboxNode, ICollideable
{
    public event Action OnScoreDetected;

    public override MapCollisionType CollisionType => MapCollisionType.Score;

    public override void NotifyCollision ()
    {
        base.NotifyCollision();
        OnScoreDetected?.Invoke();
    }
}