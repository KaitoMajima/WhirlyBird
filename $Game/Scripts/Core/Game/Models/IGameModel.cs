using System;

public interface IGameModel : IDisposable
{
    IMusicManagerModel MusicManagerModel { get; }

    void Initialize ();
}