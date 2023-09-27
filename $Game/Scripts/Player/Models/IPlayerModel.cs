using System;

public interface IPlayerModel : IDisposable
{
    event Action OnPlayerScored;
    event Action OnPlayerDamaged;
    event Action OnPlayerKilled;
    event Action OnPlayerTransformed;
    
    float PlayerSize { get; }
    float GravityScale { get; }
    float JumpStrength { get; }

    void Initialize ();
    void Score ();
    void Damage ();
    void Kill ();
}