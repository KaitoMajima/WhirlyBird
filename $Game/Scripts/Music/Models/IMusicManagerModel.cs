using System;
using Godot;

public interface IMusicManagerModel : IDisposable
{
    event Action<MusicClipEntryResource> OnMusicPlayTriggered;
    event Action OnMusicResumeTriggered;
    event Action OnMusicPauseTriggered;
        
    event Action OnMusicCrossfadeBegin;
    event Action OnMusicCrossfadeStep;
    event Action OnMusicCrossfadeEnd;
    
    float MainMusicVolume { get; }
    float TempMusicVolume { get; }

    void Setup (
        IGameStateProvider gameStateProvider, 
        MusicResource musicResource
    );

    void Initialize ();
    
    void Play (MusicClipType clipType);
    void Resume ();
    void Pause ();

    void Crossfade (
        Timer timer,
        MusicClipEntryResource clipEntry,
        float pastVolume
    );

    void ProcessFading (double delta);
}