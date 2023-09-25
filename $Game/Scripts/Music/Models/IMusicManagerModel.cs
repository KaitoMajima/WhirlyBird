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
    
    float CurrentMusicVolume { get; }
    float TempMusicVolume { get; }

    void Setup (MusicResource musicResource);
    
    void Play (MusicClipType clipType);
    void Resume ();
    void Pause ();

    void Crossfade (
        Timer timer,
        float crossfadeTime,
        float clipMaxVolume,
        float currentMusicVolume
    );

    void ProcessFading (double delta);
}