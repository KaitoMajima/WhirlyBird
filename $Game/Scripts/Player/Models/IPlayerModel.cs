public interface IPlayerModel
{
    float PlayerSize { get; }
    float GravityScale { get; }
    float JumpStrength { get; }

    void Score ();
    void Damage ();
}