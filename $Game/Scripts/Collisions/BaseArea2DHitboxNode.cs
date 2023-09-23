using Godot;

public abstract partial class BaseArea2DHitboxNode : Area2D, ICollideable
{
    public abstract MapCollisionType CollisionType { get; }
        
    public int TotalCollisions { get; private set; }
        
    public virtual void ResetView ()
    {
        TotalCollisions = 0;
    }

    public virtual void NotifyCollision ()
    {
        TotalCollisions++;
    }
}