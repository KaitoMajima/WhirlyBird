using System;
using Godot;

public class MusicManagerModel : IMusicManagerModel
{
    const int VOLUME_DB_MIN_THRESHOLD_VALUE = -33;
    
    public event Action<MusicClipEntryResource> OnMusicPlayTriggered;
    public event Action OnMusicResumeTriggered;
    public event Action OnMusicPauseTriggered;

    public event Action OnMusicCrossfadeBegin;
    public event Action OnMusicCrossfadeStep;
    public event Action OnMusicCrossfadeEnd;
    
    public float MainMusicVolume { get; private set; }
    public float TempMusicVolume { get; private set; }

    float CurrentMaxVolume => currentClipEntry.MaxVolume;
    float CurrentCrossFadeTime => currentClipEntry.CrossfadeTimeOnPlay;

    IGameStateProvider gameStateProvider;
    MusicResource musicResource;
    Timer currentTimer;
    MusicClipEntryResource currentClipEntry;
    float pastMusicVolume;
    double currentTimeStep;
    bool isFading;

    public void Setup (
        IGameStateProvider gameStateProvider, 
        MusicResource musicResource
    )
    {
        this.gameStateProvider = gameStateProvider;
        this.musicResource = musicResource;
    }

    public void Initialize ()
    {
        AddGameStateListeners();
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
        MusicClipEntryResource clipEntry,
        float pastVolume
    )
    {
        isFading = true;
        currentClipEntry = clipEntry;
        pastMusicVolume = pastVolume;
        UpdateTimer(timer);
        
        OnMusicCrossfadeBegin?.Invoke();
    }

    public void ProcessFading (double delta)
    {
        if (!isFading)
            return;
        
        MainMusicVolume = (float)Mathf.Lerp(VOLUME_DB_MIN_THRESHOLD_VALUE, CurrentMaxVolume, currentTimeStep / CurrentCrossFadeTime);
        TempMusicVolume = (float)Mathf.Lerp(pastMusicVolume, VOLUME_DB_MIN_THRESHOLD_VALUE, currentTimeStep / CurrentCrossFadeTime);
        currentTimeStep += delta;
        OnMusicCrossfadeStep?.Invoke();
    }
    
    void UpdateTimer (Timer timer)
    {
        RemoveTimerListeners();
        currentTimer = timer;
        currentTimer.WaitTime = CurrentCrossFadeTime;
        currentTimer.Start();
        AddTimerListeners();
    }
    
    void HandleGamePauseTriggered ()
    {
        if (gameStateProvider.IsGamePaused)
            OnMusicPauseTriggered?.Invoke();
        else
            OnMusicResumeTriggered?.Invoke();
    }
    
    void HandleTimerTimeout ()
    {
        MainMusicVolume = CurrentMaxVolume;
        TempMusicVolume = VOLUME_DB_MIN_THRESHOLD_VALUE;
        currentTimeStep = 0;
        currentTimer.Stop();
        currentClipEntry = null;
        OnMusicCrossfadeStep?.Invoke();

        isFading = false;
        OnMusicCrossfadeEnd?.Invoke();
    }
    
    void AddGameStateListeners ()
    {
        gameStateProvider.OnGamePauseTriggered += HandleGamePauseTriggered;
    }

    void AddTimerListeners ()
    {
        currentTimer.Timeout += HandleTimerTimeout;
    }
    
    void RemoveGameStateListeners ()
    {
        gameStateProvider.OnGamePauseTriggered -= HandleGamePauseTriggered;
    }
    
    void RemoveTimerListeners ()
    {
        if (currentTimer != null)
            currentTimer.Timeout -= HandleTimerTimeout;
    }

    public void Dispose ()
    {
        RemoveGameStateListeners();
        RemoveTimerListeners();
    }
}