using Godot;

public partial class GameOverNode : Control
{
    [Export(PropertyHint.File, "*.tscn")] 
    string mainMenuScenePath;

    [Export] GameOverCenterButtons centerButtons;
    [Export] Node sceneToUnload;

    IGameOverModel model;

    public void Setup (IGameOverModel model)
    {
        this.model = model;
    }

    public void Initialize ()
    {
        centerButtons.Initialize();
        Visible = false;
        AddModelListeners();
        AddNodeListeners();
    }

    void HandleGameOverTriggered ()
    {
        Visible = true;
    }
    
    void HandleRetryButtonPressed ()
    {
        LoadingScope.Instance.Load(sceneToUnload.SceneFilePath, sceneToUnload);
    }
    
    void HandleMainMenuButtonPressed ()
    {
        LoadingScope.Instance.Load(mainMenuScenePath, sceneToUnload);
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
    
    void RemoveModelListeners ()
    {
        model.OnGameOverTriggered -= HandleGameOverTriggered;
    }
    
    void RemoveNodeListeners ()
    {
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