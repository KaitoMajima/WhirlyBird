using Godot;

public partial class PlayerController : RigidBody2D
{
    [Export] 
    TweenManager jumpingAnimation;
    
    [Export] 
    TweenManager rotateAnimation;

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
    }
    
    void ApplyJump ()
    {
        LinearVelocity = Vector2.Zero;
        ApplyImpulse(Vector2.Up * playerModel.JumpStrength);
        jumpingAnimation.PlayTween();
        rotateAnimation.PlayTween();
    }
    
    void HandleMainActionTriggered (InputType type)
    {
        if (type == InputType.JustReleased)
            ApplyJump();
    }
    
    void AddModelListeners ()
    {
        inputDetectionModel.OnMainActionTriggered += HandleMainActionTriggered;
    }

    void RemoveModelListeners ()
    {
        inputDetectionModel.OnMainActionTriggered -= HandleMainActionTriggered;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}