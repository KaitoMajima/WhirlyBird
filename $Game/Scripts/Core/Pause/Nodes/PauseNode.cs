using Godot;

public partial class PauseNode : Control, IPauseNode
{
    [Export]
    NodePath CenterButtonsPath { get; set; }
    
    [Export]
    NodePath SceneToUnloadPath { get; set; }
    
    [Export(PropertyHint.File, "*.tscn")]
    string MainMenuScenePath { get; set; }
    
    public PauseMenuCenterButtons CenterButtons { get; private set; }

    IPauseModel pauseModel;
    Node sceneToUnload;

    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }

    public void Initialize ()
    {
        sceneToUnload = GetNode<Node>(SceneToUnloadPath);
        CenterButtons = GetNode<PauseMenuCenterButtons>(CenterButtonsPath);
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