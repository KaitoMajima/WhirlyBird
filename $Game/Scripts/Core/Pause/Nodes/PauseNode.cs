using Godot;

public partial class PauseNode : Control, IPauseNode
{
    [Export]
    public PauseMenuCenterButtons CenterButtons { get; set; }
    
    [Export] 
    Node SceneToUnload { get; set; }
    
    [Export(PropertyHint.File, "*.tscn")]
    string MainMenuScenePath { get; set; }
    
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
        LoadingScope.Instance.Load(SceneToUnload.SceneFilePath, SceneToUnload);
    }
    
    void HandleMainMenuButtonPressed ()
    {
        pauseModel.SetPause(false);
        LoadingScope.Instance.Load(MainMenuScenePath, SceneToUnload);
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        RemoveNodeListeners();
        CenterButtons.Dispose();
        base.Dispose();
    }
}