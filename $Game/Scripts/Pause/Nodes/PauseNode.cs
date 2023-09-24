using Godot;

public partial class PauseNode : Control
{
    [Export(PropertyHint.File, "*.tscn")] 
    string mainMenuScenePath;

    [Export] PauseMenuCenterButtons centerButtons;
    [Export] Node sceneToUnload;
    
    IPauseModel pauseModel;

    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }

    public void Initialize ()
    {
        centerButtons.Initialize();
        Visible = false;
        AddModelListeners();
        AddNodeListeners();
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
        LoadingScope.Instance.Load(mainMenuScenePath, sceneToUnload);
    }
    
    void AddModelListeners ()
    {
        pauseModel.OnPauseTriggered += HandlePauseTriggered;
    }

    void AddNodeListeners ()
    {
        centerButtons.OnResumeButtonPressed += HandleResumeButtonPressed;
        centerButtons.OnRetryButtonPressed += HandleRetryButtonPressed;
        centerButtons.OnMainMenuButtonPressed += HandleMainMenuButtonPressed;
    }

    void RemoveModelListeners ()
    {
        pauseModel.OnPauseTriggered -= HandlePauseTriggered;
    }
    
    void RemoveNodeListeners ()
    {
        centerButtons.OnResumeButtonPressed -= HandleResumeButtonPressed;
        centerButtons.OnRetryButtonPressed -= HandleRetryButtonPressed;
        centerButtons.OnMainMenuButtonPressed -= HandleMainMenuButtonPressed;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        RemoveNodeListeners();
        centerButtons.Dispose();
        base.Dispose();
    }
}