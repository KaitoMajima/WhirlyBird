using System;
using Godot;

public partial class PlayerManagerNode : Node2D
{
    [Export] float rotateAnimationMin = 90;
    [Export] float rotateAnimationMultiplier = 0.77f;
    [Export] Scale2DTween jumpingAnimation;
    [Export] Rotate2DTween rotateAnimation;
    [Export] RigidBody2D rigidBody2D;
    [Export] Area2D collisionDetector;
    [Export] Node2D contentsTransform;
    [Export] Node2D rigidBodyShapeTransform;

    IPlayerModel playerModel;
    IMapInputDetectionModel inputDetectionModel;
    IRandomProvider randomProvider;
    
    public void Setup (
        IPlayerModel playerModel,
        IMapInputDetectionModel inputDetectionModel,
        IRandomProvider randomProvider
    )
    {
        this.playerModel = playerModel;
        this.inputDetectionModel = inputDetectionModel;
        this.randomProvider = randomProvider;
    }

    public void Initialize ()
    {
        SetupPlayerSize();
        SetupPlayerGravityScale();
        AddModelListeners();
        AddRigidBodyListeners();
    }
    
    void SetupPlayerSize ()
    {
        Vector2 playerSize = new(playerModel.PlayerSize, playerModel.PlayerSize);
        contentsTransform.Scale = playerSize;
        rigidBodyShapeTransform.Scale = playerSize;
    }
    
    void SetupPlayerGravityScale ()
    {
        rigidBody2D.GravityScale = playerModel.GravityScale;
    }

    void ApplyJump ()
    {
        float originalYVelocity = Mathf.Abs(rigidBody2D.LinearVelocity.Y);
        rigidBody2D.LinearVelocity = Vector2.Zero;
        rigidBody2D.ApplyImpulse(Vector2.Up * playerModel.JumpStrength);
        jumpingAnimation.PlayTween();
        float rotationDegrees = Mathf.Max(rotateAnimationMin, originalYVelocity * rotateAnimationMultiplier);
        rotateAnimation.TargetRotationDegrees = rotationDegrees;
        rotateAnimation.PlayTween();
    }
    
    void ApplyDeathThrow ()
    {
        rigidBody2D.LinearVelocity = Vector2.Zero;
        float randomXDirection = randomProvider.Range(-1, 1);
        float randomYDirection = randomProvider.Range(-1, 1);
        float randomXForce = randomProvider.Range(420, 4200);
        float randomYForce = randomProvider.Range(420, 4200);

        Vector2 throwVector = new(
            randomXDirection * randomXForce,
            randomYDirection * randomYForce
        );

        rigidBody2D.ApplyImpulse(throwVector);
    }
    
    void ProcessCollisions (Node2D body)
    {
        ICollideable collideable = (ICollideable)body;
        collideable.NotifyCollision();

        switch (collideable.CollisionType)
        {
            case MapCollisionType.Score:
                if (collideable.TotalCollisions > 1)
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
    
    void HandlePlayerKilled ()
    {
        inputDetectionModel.LockAllInputs();
        ApplyDeathThrow();
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
        playerModel.OnPlayerKilled += HandlePlayerKilled;
        inputDetectionModel.OnMainActionTriggered += HandleMainActionTriggered;
    }

    void AddRigidBodyListeners ()
    {
        collisionDetector.AreaEntered += HandleTriggerEntered;
        collisionDetector.BodyEntered += HandleColliderEntered;
    }
    
    void RemoveModelListeners ()
    {
        playerModel.OnPlayerKilled -= HandlePlayerKilled;
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