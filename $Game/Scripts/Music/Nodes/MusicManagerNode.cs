using Godot;

public partial class MusicManagerNode : Node
{
    [Export] public AudioStreamPlayer MainMusicPlayer { get; private set; }
    [Export] public AudioStreamPlayer TempMusicPlayer { get; private set; }
    [Export] Timer crossfadeTimer;

    IMusicManagerModel model;
    MusicClipEntryResource currentClipEntry;

    public override void _Process (double delta)
    {
        model.ProcessFading(delta);
    }

    public void Setup (IMusicManagerModel model)
    {
        this.model = model;
    }

    public void Initialize ()
    {
        AddModelListeners();
    }

    void HandleMusicPlayTriggered (MusicClipEntryResource clipEntry)
    {
        currentClipEntry = clipEntry;
        model.Crossfade(
            crossfadeTimer,
            currentClipEntry,
            MainMusicPlayer.VolumeDb
        );
    }

    void HandleMusicResumeTriggered ()
    {
        MainMusicPlayer.StreamPaused = false;
        TempMusicPlayer.StreamPaused = false;
    }

    void HandleMusicPauseTriggered ()
    {
        MainMusicPlayer.StreamPaused = true;
        TempMusicPlayer.StreamPaused = true;
    }

    void HandleMusicCrossfadeBegin ()
    {
        TempMusicPlayer.Stream = MainMusicPlayer.Stream;
        TempMusicPlayer.Play(MainMusicPlayer.GetPlaybackPosition());
        
        MainMusicPlayer.Stream = currentClipEntry.AudioStream;
        MainMusicPlayer.Play(currentClipEntry.Offset);
    }

    void HandleMusicCrossfadeStep ()
    {
        MainMusicPlayer.VolumeDb = model.MainMusicVolume;
        TempMusicPlayer.VolumeDb = model.TempMusicVolume;
    }

    void HandleMusicCrossfadeEnd ()
    {
        TempMusicPlayer.Stop();
    }
    
    void AddModelListeners ()
    {
        model.OnMusicPlayTriggered += HandleMusicPlayTriggered;
        model.OnMusicResumeTriggered += HandleMusicResumeTriggered;
        model.OnMusicPauseTriggered += HandleMusicPauseTriggered;
        model.OnMusicCrossfadeBegin += HandleMusicCrossfadeBegin;
        model.OnMusicCrossfadeStep += HandleMusicCrossfadeStep;
        model.OnMusicCrossfadeEnd += HandleMusicCrossfadeEnd;
    }

    void RemoveModelListeners ()
    {
        model.OnMusicPlayTriggered -= HandleMusicPlayTriggered;
        model.OnMusicResumeTriggered -= HandleMusicResumeTriggered;
        model.OnMusicPauseTriggered -= HandleMusicPauseTriggered;
        model.OnMusicCrossfadeBegin -= HandleMusicCrossfadeBegin;
        model.OnMusicCrossfadeStep -= HandleMusicCrossfadeStep;
        model.OnMusicCrossfadeEnd -= HandleMusicCrossfadeEnd;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}