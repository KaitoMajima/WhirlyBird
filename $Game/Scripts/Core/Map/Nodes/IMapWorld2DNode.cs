using System;

public interface IMapWorld2DNode : IDisposable
{
    PlayerController PlayerController { get; }
    
    void Setup (IPlayerModel playerModel);
    void Initialize ();
}