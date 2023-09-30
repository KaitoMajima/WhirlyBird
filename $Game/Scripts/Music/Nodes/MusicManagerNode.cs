using Godot;

public partial class MusicManagerNode : Node
{
    [Export] public AudioStreamPlayer MainMusicPlayer { get; private set; }
    [Export] public AudioStreamPlayer TempMusicPlayer { get; private set; }
    [Export] TestableTimer crossfadeTimer;

    IMusicManagerSystem system;
    MusicClipEntryResource currentClipEntry;

    public override void _Process (double delta)
    {
        system.ProcessFading(delta);
    }

    public void Setup (IMusicManagerSystem system)
    {
        this.system = system;
        system.SetTimer(crossfadeTimer);
    }

    public void Initialize ()
    {
        AddModelListeners();
    }

    void HandleMusicPlayTriggered (MusicClipEntryResource clipEntry)
    {
        currentClipEntry = clipEntry;
        system.Crossfade(
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
        MainMusicPlayer.VolumeDb = system.MainMusicVolume;
        TempMusicPlayer.VolumeDb = system.TempMusicVolume;
    }

    void HandleMusicCrossfadeEnd ()
    {
        TempMusicPlayer.Stop();
    }
    
    void AddModelListeners ()
    {
        system.OnMusicPlayTriggered += HandleMusicPlayTriggered;
        system.OnMusicResumeTriggered += HandleMusicResumeTriggered;
        system.OnMusicPauseTriggered += HandleMusicPauseTriggered;
        system.OnMusicCrossfadeBegin += HandleMusicCrossfadeBegin;
        system.OnMusicCrossfadeStep += HandleMusicCrossfadeStep;
        system.OnMusicCrossfadeEnd += HandleMusicCrossfadeEnd;
    }

    void RemoveModelListeners ()
    {
        system.OnMusicPlayTriggered -= HandleMusicPlayTriggered;
        system.OnMusicResumeTriggered -= HandleMusicResumeTriggered;
        system.OnMusicPauseTriggered -= HandleMusicPauseTriggered;
        system.OnMusicCrossfadeBegin -= HandleMusicCrossfadeBegin;
        system.OnMusicCrossfadeStep -= HandleMusicCrossfadeStep;
        system.OnMusicCrossfadeEnd -= HandleMusicCrossfadeEnd;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}