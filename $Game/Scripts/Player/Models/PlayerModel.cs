using Godot;

public class PlayerModel : IPlayerModel
{
    const float JUMP_MULTIPLIER = 59;
    const float GRAVITY_SCALE_MULTIPLIER = 0.6f;
    
    public float PlayerSize => playerSettings.PlayerSize;
    public float GravityScale => playerSettings.PlayerGravityScale * GRAVITY_SCALE_MULTIPLIER;
    public float JumpStrength => playerSettings.PlayerJumpStrength * JUMP_MULTIPLIER;
    
    public void Score ()
    {
        GD.Print("Player has scored!");
    }

    public void Damage ()
    {
        GD.Print("Player has been damaged!");
    }

    readonly IPlayerSettings playerSettings;

    public PlayerModel (IPlayerSettings playerSettings)
    {
        this.playerSettings = playerSettings;
    }
}