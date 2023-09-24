using System;

public class PlayerModel : IPlayerModel
{
    public event Action OnPlayerScored;
    public event Action OnPlayerDamaged;
    public event Action OnPlayerKilled;
    
    public float PlayerSize => playerSettings.PlayerSize;
    public float GravityScale => playerSettings.PlayerGravityScale;
    public float JumpStrength => playerSettings.PlayerJumpStrength;
    
    public bool IsPlayerKilled { get; private set; }

    readonly IPlayerSettings playerSettings;

    public PlayerModel (IPlayerSettings playerSettings)
    {
        this.playerSettings = playerSettings;
    }
    
    public void Score ()
    {
        OnPlayerScored?.Invoke();
    }

    public void Damage ()
    {
        OnPlayerDamaged?.Invoke();
    }

    public void Kill ()
    {
        IsPlayerKilled = true;
        OnPlayerKilled?.Invoke();
    }
}