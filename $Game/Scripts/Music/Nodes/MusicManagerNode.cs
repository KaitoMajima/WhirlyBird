using Godot;

public partial class MusicManagerNode : Node
{
    [Export] AudioStreamPlayer mainMusicPlayer;
    [Export] AudioStreamPlayer tempMusicPlayer;
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
            mainMusicPlayer.VolumeDb
        );
    }

    void HandleMusicResumeTriggered ()
    {
        mainMusicPlayer.Play();
        tempMusicPlayer.Play();
    }

    void HandleMusicPauseTriggered ()
    {
        mainMusicPlayer.Stop();
        tempMusicPlayer.Stop();
    }

    void HandleMusicCrossfadeBegin ()
    {
        tempMusicPlayer.Stream = mainMusicPlayer.Stream;
        tempMusicPlayer.Play(mainMusicPlayer.GetPlaybackPosition());
        
        mainMusicPlayer.Stream = currentClipEntry.AudioStream;
        mainMusicPlayer.Play(currentClipEntry.Offset);
    }

    void HandleMusicCrossfadeStep ()
    {
        mainMusicPlayer.VolumeDb = model.MainMusicVolume;
        tempMusicPlayer.VolumeDb = model.TempMusicVolume;
    }

    void HandleMusicCrossfadeEnd ()
    {
        tempMusicPlayer.Stop();
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