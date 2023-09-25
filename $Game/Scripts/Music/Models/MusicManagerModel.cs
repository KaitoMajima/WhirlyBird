using System;
using Godot;

public class MusicManagerModel : IMusicManagerModel
{
    public event Action<MusicClipEntryResource> OnMusicPlayTriggered;
    public event Action OnMusicResumeTriggered;
    public event Action OnMusicPauseTriggered;

    public event Action OnMusicCrossfadeBegin;
    public event Action OnMusicCrossfadeStep;
    public event Action OnMusicCrossfadeEnd;
    
    public float CurrentMusicVolume { get; private set; }
    public float TempMusicVolume { get; private set; }
    
    MusicResource musicResource;
    Timer currentTimer;
    bool isFading;
    double currentTimeStep;
    double currentClipCrossfadeTime;
    float currentClipMaxVolume;
    float currentClipMusicVolume;

    public void Setup (MusicResource musicResource)
    {
        this.musicResource = musicResource;
    }

    public void Play (MusicClipType clipType)
    {
        MusicClipEntryResource musicClipEntry = musicResource.GetByType(clipType);
        OnMusicPlayTriggered?.Invoke(musicClipEntry);
    }

    public void Resume ()
    {
        OnMusicResumeTriggered?.Invoke();
    }

    public void Pause ()
    {
        OnMusicPauseTriggered?.Invoke();
    }

    public void Crossfade (
        Timer timer, 
        float crossfadeTime, 
        float clipMaxVolume,
        float currentMusicVolume
    )
    {
        isFading = true;
        currentClipCrossfadeTime = crossfadeTime;
        currentClipMaxVolume = clipMaxVolume;
        currentClipMusicVolume = currentMusicVolume;
        UpdateTimer(timer, crossfadeTime);
        
        OnMusicCrossfadeBegin?.Invoke();
    }

    public void ProcessFading (double delta)
    {
        if (!isFading)
            return;
        
        currentTimeStep += delta;
        
        CurrentMusicVolume = (float)Mathf.Lerp(0, currentClipMaxVolume, currentClipCrossfadeTime / currentTimeStep);
        TempMusicVolume = (float)Mathf.Lerp(currentClipMusicVolume, 0, currentTimeStep / currentClipCrossfadeTime);
        OnMusicCrossfadeStep?.Invoke();
    }
    
    void UpdateTimer (Timer timer, float crossfadeTime)
    {
        RemoveTimerListeners();
        currentTimer = timer;
        currentTimer.WaitTime = crossfadeTime;
        currentTimer.Start();
        AddTimerListeners();
    }
    
    void HandleTimerTimeout ()
    {
        CurrentMusicVolume = currentClipMaxVolume;
        TempMusicVolume = 0;
        OnMusicCrossfadeStep?.Invoke();

        isFading = false;
        OnMusicCrossfadeEnd?.Invoke();
    }
    
    void AddTimerListeners ()
    {
        currentTimer.Timeout += HandleTimerTimeout;
    }
    
    void RemoveTimerListeners ()
    {
        if (currentTimer != null)
            currentTimer.Timeout -= HandleTimerTimeout;
    }

    public void Dispose ()
    {
        RemoveTimerListeners();
    }
}