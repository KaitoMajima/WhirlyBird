using System;

public interface IPlayerModel
{
    event Action OnPlayerScored;
    
    float PlayerSize { get; }
    float GravityScale { get; }
    float JumpStrength { get; }

    void Score ();
    void Damage ();
}