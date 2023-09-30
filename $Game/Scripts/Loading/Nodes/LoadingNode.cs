using Godot;

public partial class LoadingNode : Node
{
    [Export]
    public LoadingConfigResource LoadingConfigResource { get; private set; }

    [Export] Control raycastBlocker;
    [Export] AnimationPlayer animationPlayer;
    [Export] ProgressBar progressBar;
    [Export] StringName fadeInAnimation;
    [Export] StringName fadeOutAnimation;

    ILoadingSystem system;
    
    public override void _Process (double delta)
    {
        if (system.IsLoading)
            system.ProcessLoading();
    }

    public void Setup (ILoadingSystem system)
    {
        this.system = system;
    }

    public void Initialize ()
    {
        AddModelListeners();
        AddAnimationListeners();
    }

    void SetLoadingProgress (double progress)
    {
        progressBar.Value = progress;
    }
    
    void FadeIn ()
    {
        animationPlayer.Play(fadeInAnimation);
        raycastBlocker.MouseFilter = Control.MouseFilterEnum.Stop;
    }
    
    void FadeOut ()
    {
        animationPlayer.Play(fadeOutAnimation);
        raycastBlocker.MouseFilter = Control.MouseFilterEnum.Ignore;
    }
    
    void HandleLoadingStarted ()
    {
        SetLoadingProgress(0);
        FadeIn();
    }

    void HandleLoadingProgress (double progress)
    {
        SetLoadingProgress(progress);
    }

    void HandleLoadingFinished (PackedScene scene)
    {
        Node sceneNode = scene.Instantiate();
        GetTree().Root.AddChild(sceneNode);
        FadeOut();
        SetLoadingProgress(100);
    }

    void HandleAnimationFinished (StringName animationName)
    {
        if (animationName == fadeInAnimation)
            system.InitiateLoading();
    }
    
    void AddModelListeners ()
    {
        system.OnLoadingStarted += HandleLoadingStarted;
        system.OnLoadingProgress += HandleLoadingProgress;
        system.OnLoadingFinished += HandleLoadingFinished;
    }

    void AddAnimationListeners ()
    {
        animationPlayer.AnimationFinished += HandleAnimationFinished;
    }
    
    void RemoveModelListeners ()
    {
        system.OnLoadingStarted -= HandleLoadingStarted;
        system.OnLoadingProgress -= HandleLoadingProgress;
        system.OnLoadingFinished -= HandleLoadingFinished;
    }
    
    void RemoveAnimationListeners ()
    {
        animationPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    public new void Dispose ()
    {
        RemoveAnimationListeners();
        base.Dispose();
    }
}