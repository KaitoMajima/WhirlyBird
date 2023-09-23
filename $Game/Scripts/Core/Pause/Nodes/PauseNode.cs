using Godot;

public partial class PauseNode : Control
{
    [Export]
    public PauseMenuCenterButtons CenterButtons { get; set; }

    [Export(PropertyHint.File, "*.tscn")]
    string MainMenuScenePath { get; set; }
    
    [Export] Node sceneToUnload;
    
    IPauseModel pauseModel;

    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }

    public void Initialize ()
    {
        CenterButtons.Initialize();
        Visible = false;
        AddModelListeners();
        AddNodeListeners();
    }

    void AddModelListeners ()
    {
        pauseModel.OnPauseTriggered += HandlePauseTriggered;
    }

    void AddNodeListeners ()
    {
        CenterButtons.OnResumeButtonPressed += HandleResumeButtonPressed;
        CenterButtons.OnRetryButtonPressed += HandleRetryButtonPressed;
        CenterButtons.OnMainMenuButtonPressed += HandleMainMenuButtonPressed;
    }

    void RemoveModelListeners ()
    {
        pauseModel.OnPauseTriggered -= HandlePauseTriggered;
    }
    
    void RemoveNodeListeners ()
    {
        CenterButtons.OnResumeButtonPressed -= HandleResumeButtonPressed;
        CenterButtons.OnRetryButtonPressed -= HandleRetryButtonPressed;
        CenterButtons.OnMainMenuButtonPressed -= HandleMainMenuButtonPressed;
    }
    
    void HandlePauseTriggered ()
    {
        Visible = pauseModel.IsPaused;
    }

    void HandleResumeButtonPressed ()
    {
        pauseModel.SetPause(false);
    }
    
    void HandleRetryButtonPressed ()
    {
        pauseModel.SetPause(false);
        LoadingScope.Instance.Load(sceneToUnload.SceneFilePath, sceneToUnload);
    }
    
    void HandleMainMenuButtonPressed ()
    {
        pauseModel.SetPause(false);
        LoadingScope.Instance.Load(MainMenuScenePath, sceneToUnload);
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        RemoveNodeListeners();
        CenterButtons.Dispose();
        base.Dispose();
    }
}