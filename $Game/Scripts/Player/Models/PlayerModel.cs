using System;
using Godot;

public class PlayerModel : IPlayerModel
{
    public event Action OnPlayerScored;
    
    public float PlayerSize => playerSettings.PlayerSize;
    public float GravityScale => playerSettings.PlayerGravityScale;
    public float JumpStrength => playerSettings.PlayerJumpStrength;
    
    public void Score ()
    {
        OnPlayerScored?.Invoke();
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