﻿public interface ICollideable
{
    MapCollisionType CollisionType { get; } 
    int TotalCollisions { get; }

    void ResetView ();
    void NotifyCollision ();
}