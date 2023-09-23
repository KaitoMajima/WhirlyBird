using System;
using Godot;

public partial class PlayerController : RigidBody2D
{
    [Export] 
    TweenManager jumpingAnimation;
    
    [Export] 
    TweenManager rotateAnimation;

    [Export] 
    Area2D collisionDetector;

    IPlayerModel playerModel;
    IMapInputDetectionModel inputDetectionModel;
    
    public void Setup (
        IPlayerModel playerModel,
        IMapInputDetectionModel inputDetectionModel
    )
    {
        this.playerModel = playerModel;
        this.inputDetectionModel = inputDetectionModel;
    }

    public void Initialize ()
    {
        GravityScale = playerModel.GravityScale;
        AddModelListeners();
        AddRigidBodyListeners();
    }

    void ApplyJump ()
    {
        LinearVelocity = Vector2.Zero;
        ApplyImpulse(Vector2.Up * playerModel.JumpStrength);
        jumpingAnimation.PlayTween();
        rotateAnimation.PlayTween();
    }
    
    void ProcessCollisions (Node2D body)
    {
        ICollideable collideable = (ICollideable)body;
        collideable.NotifyCollision();

        switch (collideable.CollisionType)
        {
            case MapCollisionType.Score:
                if (collideable.TotalCollisions > 0)
                    break;
                playerModel.Score();
                break;
            case MapCollisionType.Damage:
                playerModel.Damage();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void HandleMainActionTriggered (InputType type)
    {
        if (type == InputType.JustReleased)
            ApplyJump();
    }
    
    void HandleColliderEntered (Node2D body)
    {
        if (body is not ICollideable)
            return;

        ProcessCollisions(body);
    }

    void HandleTriggerEntered (Area2D area)
    {
        if (area is not ICollideable)
            return;

        ProcessCollisions(area);
    }
    
    void AddModelListeners ()
    {
        inputDetectionModel.OnMainActionTriggered += HandleMainActionTriggered;
    }
    
    void AddRigidBodyListeners ()
    {
        collisionDetector.AreaEntered += HandleTriggerEntered;
        collisionDetector.BodyEntered += HandleColliderEntered;
    }
    
    void RemoveModelListeners ()
    {
        inputDetectionModel.OnMainActionTriggered -= HandleMainActionTriggered;
    }

    void RemoveRigidBodyListeners ()
    {
        collisionDetector.AreaEntered -= HandleTriggerEntered;
        collisionDetector.BodyEntered -= HandleColliderEntered;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}