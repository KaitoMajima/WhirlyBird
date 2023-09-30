using System;

public interface IGameModel : IDisposable
{
    IMusicManagerSystem MusicManagerSystem { get; }

    void Initialize ();
}