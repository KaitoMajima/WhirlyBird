using Godot;

public partial class PlayerController : RigidBody2D
{
    [Export] 
    InputEventAction inputEventAction;

    IPlayerModel playerModel;
    
    public override void _Process (double delta)
    {
        if (Input.IsActionJustPressed(inputEventAction.Action))
            ApplyJump();
    }

    public void Setup (IPlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }

    public void Initialize ()
    {
        GravityScale = playerModel.GravityScale;
    }

    void ApplyJump ()
    {
        LinearVelocity = Vector2.Zero;
        ApplyImpulse(Vector2.Up * playerModel.JumpStrength);
    }
}