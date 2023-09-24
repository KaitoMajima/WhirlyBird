using Godot;

public partial class GameOverNode : Control
{
    [Export(PropertyHint.File, "*.tscn")] 
    string mainMenuScenePath;

    [Export] GameOverCenterButtons centerButtons;
    [Export] Node sceneToUnload;
    [Export] Timer buttonInputActivationTimer;
    [Export] TweenManager[] gameOverAnimations;

    IGameOverModel model;

    public void Setup (IGameOverModel model)
    {
        this.model = model;
    }

    public void Initialize ()
    {
        centerButtons.Initialize();
        centerButtons.SetMouseFilter(MouseFilterEnum.Ignore);
        Visible = false;
        AddModelListeners();
        AddNodeListeners();
    }

    void HandleGameOverTriggered ()
    {
        Visible = true;
        foreach (TweenManager gameOverAnimation in gameOverAnimations)
            gameOverAnimation.PlayTween();
        
        buttonInputActivationTimer.Start();
        AddTimerListeners();
    }
    
    void HandleRetryButtonPressed ()
    {
        LoadingScope.Instance.Load(sceneToUnload.SceneFilePath, sceneToUnload);
    }
    
    void HandleMainMenuButtonPressed ()
    {
        LoadingScope.Instance.Load(mainMenuScenePath, sceneToUnload);
    }
    
    void HandleButtonActivationTimeout ()
    {
        centerButtons.SetMouseFilter(MouseFilterEnum.Stop);
    }

    void AddModelListeners ()
    {
        model.OnGameOverTriggered += HandleGameOverTriggered;
    }
    
    void AddNodeListeners ()
    {
        centerButtons.OnRetryButtonPressed += HandleRetryButtonPressed;
        centerButtons.OnMainMenuButtonPressed += HandleMainMenuButtonPressed;
    }

    void AddTimerListeners ()
    {
        buttonInputActivationTimer.Timeout += HandleButtonActivationTimeout;
    }
    
    void RemoveModelListeners ()
    {
        model.OnGameOverTriggered -= HandleGameOverTriggered;
    }
    
    void RemoveNodeListeners ()
    {
        centerButtons.OnRetryButtonPressed -= HandleRetryButtonPressed;
        centerButtons.OnMainMenuButtonPressed -= HandleMainMenuButtonPressed;
    }
    
    void RemoveTimerListeners ()
    {
        buttonInputActivationTimer.Timeout -= HandleButtonActivationTimeout;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        RemoveNodeListeners();
        RemoveTimerListeners();
        centerButtons.Dispose();
        base.Dispose();
    }
}