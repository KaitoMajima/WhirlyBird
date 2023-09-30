public class LevelChangeMusicModel : ILevelChangeMusicModel
{
    readonly ILevelChangeModel levelChangeModel;
    readonly IMusicManagerSystem musicManagerSystem;

    public LevelChangeMusicModel (
        ILevelChangeModel levelChangeModel, 
        IMusicManagerSystem musicManagerSystem
    )
    {
        this.levelChangeModel = levelChangeModel;
        this.musicManagerSystem = musicManagerSystem;
    }

    public void Initialize ()
    {
        HandleLevelChanged();
        AddLevelChangeListeners();
    }
    
    void HandleLevelChanged ()
    {
        switch (levelChangeModel.CurrentLevelId)
        {
            case 0:
                musicManagerSystem.Play(MusicClipType.Level0);
                break;
            case 1:
                musicManagerSystem.Play(MusicClipType.Level1);
                break;
        }
    }

    void AddLevelChangeListeners ()
    {
        levelChangeModel.OnLevelChanged += HandleLevelChanged;
    }
    
    void RemoveLevelChangeListeners ()
    {
        levelChangeModel.OnLevelChanged -= HandleLevelChanged;
    }

    public void Dispose ()
    {
        RemoveLevelChangeListeners();
    }
}