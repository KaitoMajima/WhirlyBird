using Godot;

public partial class LoadingNode : Node, ILoadingNode
{
    [Export]
    NodePath AnimationPlayerPath { get; set; }
    
    [Export]
    NodePath ProgressBarPath { get; set; }

    [Export] 
    StringName FadeInAnimation { get; set; }
    
    [Export] 
    StringName FadeOutAnimation { get; set; }

    ILoadingModel model;
    
    AnimationPlayer animationPlayer;
    ProgressBar progressBar;

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
        animationPlayer = GetNode<AnimationPlayer>(AnimationPlayerPath);
        progressBar = GetNode<ProgressBar>(ProgressBarPath);
        AddModelListeners();
        AddAnimationListeners();
    }

    void SetLoadingProgress (double progress)
    {
        progressBar.Value = progress;
    }
    
    void FadeIn ()
    {
        animationPlayer.Play(FadeInAnimation);
    }
    
    void FadeOut ()
    {
        animationPlayer.Play(FadeOutAnimation);
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
        if (animationName == FadeInAnimation)
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