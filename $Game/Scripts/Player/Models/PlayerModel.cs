using Godot;

public class PlayerModel : IPlayerModel
{
    public float PlayerSize => playerSettings.PlayerSize;
    public float GravityScale => playerSettings.PlayerGravityScale;
    public float JumpStrength => playerSettings.PlayerJumpStrength;
    
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