using System;

public interface IPlayerModel
{
    event Action OnPlayerScored;
    event Action OnPlayerDamaged;
    event Action OnPlayerKilled;
    
    float PlayerSize { get; }
    float GravityScale { get; }
    float JumpStrength { get; }
    bool IsPlayerKilled { get; }

    void Score ();
    void Damage ();
    void Kill ();
}