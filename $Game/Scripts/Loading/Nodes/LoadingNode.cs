using Godot;

public partial class LoadingNode : Node
{
    [Export]
    public LoadingConfigResource LoadingConfigResource { get; private set; }

    [Export] AnimationPlayer animationPlayer;
    [Export] ProgressBar progressBar;
    [Export] StringName fadeInAnimation;
    [Export] StringName fadeOutAnimation;

    ILoadingModel model;
    
    public override void _Process (double delta)
    {
        if (model.IsLoading)
            model.ProcessLoading();
    }

    public void Setup (ILoadingModel model)
    {
        this.model = model;
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
    }
    
    void FadeOut ()
    {
        animationPlayer.Play(fadeOutAnimation);
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
            model.InitiateLoading();
    }
    
    void AddModelListeners ()
    {
        model.OnLoadingStarted += HandleLoadingStarted;
        model.OnLoadingProgress += HandleLoadingProgress;
        model.OnLoadingFinished += HandleLoadingFinished;
    }

    void AddAnimationListeners ()
    {
        animationPlayer.AnimationFinished += HandleAnimationFinished;
    }
    
    void RemoveModelListeners ()
    {
        model.OnLoadingStarted -= HandleLoadingStarted;
        model.OnLoadingProgress -= HandleLoadingProgress;
        model.OnLoadingFinished -= HandleLoadingFinished;
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