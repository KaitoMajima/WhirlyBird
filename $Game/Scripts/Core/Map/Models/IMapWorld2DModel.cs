using System;

public interface IMapWorld2DModel : IDisposable
{
    IPlayerModel PlayerModel { get; }
    
    void Initialize ();
}