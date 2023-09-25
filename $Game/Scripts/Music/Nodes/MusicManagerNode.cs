using Godot;

public partial class MusicManagerNode : Node
{
    [Export] AudioStreamPlayer currentMusicPlayer;
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
            currentClipEntry.CrossfadeTimeOnPlay,
            currentClipEntry.MaxVolume,
            currentMusicPlayer.VolumeDb
        );
    }

    void HandleMusicResumeTriggered ()
    {
        currentMusicPlayer.Play();
        tempMusicPlayer.Play();
    }

    void HandleMusicPauseTriggered ()
    {
        currentMusicPlayer.Stop();
        tempMusicPlayer.Stop();
    }

    void HandleMusicCrossfadeBegin ()
    {
        tempMusicPlayer.Stream = currentMusicPlayer.Stream;
        tempMusicPlayer.Play(currentMusicPlayer.GetPlaybackPosition());
        
        currentMusicPlayer.Stream = currentClipEntry.AudioStream;
        currentMusicPlayer.Play(currentClipEntry.Offset);
    }

    void HandleMusicCrossfadeStep ()
    {
        currentMusicPlayer.VolumeDb = model.CurrentMusicVolume;
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