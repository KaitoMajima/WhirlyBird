using Godot;

public abstract partial class BaseNode2DHitboxNode : Node2D, ICollideable
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