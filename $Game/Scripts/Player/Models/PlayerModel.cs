using System;

public class PlayerModel : IPlayerModel
{
    public event Action OnPlayerScored;
    public event Action OnPlayerDamaged;
    public event Action OnPlayerKilled;
    public event Action OnPlayerTransformed;

    public float PlayerSize => playerSettings.PlayerSize;
    public float GravityScale => playerSettings.PlayerGravityScale;
    public float JumpStrength => playerSettings.PlayerJumpStrength;

    readonly IPlayerSettings playerSettings;
    readonly ILevelChangeModel levelChangeModel;

    public PlayerModel (IPlayerSettings playerSettings, ILevelChangeModel levelChangeModel)
    {
        this.playerSettings = playerSettings;
        this.levelChangeModel = levelChangeModel;
    }

    public void Initialize ()
    {
        AddLevelChangeListeners();
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
        OnPlayerKilled?.Invoke();
    }
    
    void HandleLevelChanged ()
    {
        OnPlayerTransformed?.Invoke();
    }

    void AddLevelChangeListeners ()
    {
        levelChangeModel.OnLevelChanged += HandleLevelChanged;
    }

    void RemoveLevelChangeListeners ()
    {
        levelChangeModel.OnLevelChanged -= HandleLevelChanged;
    }

    public void Dispose ()
    {
        RemoveLevelChangeListeners();
    }
}