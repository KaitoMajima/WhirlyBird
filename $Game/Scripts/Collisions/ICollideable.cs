public interface ICollideable
{
    MapCollisionType CollisionType { get; } 
    int TotalCollisions { get; }

    void Reset ();
    void NotifyCollision ();
}