public class PlayerModel : IPlayerModel
{
    const float JUMP_MULTIPLIER = 50;
    const float GRAVITY_SCALE_MULTIPLIER = 0.5f;
    
    public float PlayerSize => playerSettings.PlayerSize;
    public float GravityScale => playerSettings.PlayerGravityScale * GRAVITY_SCALE_MULTIPLIER;
    public float JumpStrength => playerSettings.PlayerJumpStrength * JUMP_MULTIPLIER;

    readonly IPlayerSettings playerSettings;

    public PlayerModel (IPlayerSettings playerSettings)
    {
        this.playerSettings = playerSettings;
    }
}