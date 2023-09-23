using Godot;

public partial class LoadingNode : Node, ILoadingNode
{
    [Export]
    public LoadingConfigResource LoadingConfigResource { get; private set; }
    
    [Export]
    AnimationPlayer AnimationPlayer { get; set; }
    
    [Export]
    ProgressBar ProgressBar { get; set; }

    [Export] 
    StringName FadeInAnimation { get; set; }
    
    [Export] 
    StringName FadeOutAnimation { get; set; }

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
        ProgressBar.Value = progress;
    }
    
    void FadeIn ()
    {
        AnimationPlayer.Play(FadeInAnimation);
    }
    
    void FadeOut ()
    {
        AnimationPlayer.Play(FadeOutAnimation);
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
        AnimationPlayer.AnimationFinished += HandleAnimationFinished;
    }
    
    void RemoveModelListeners ()
    {
        model.OnLoadingStarted -= HandleLoadingStarted;
        model.OnLoadingProgress -= HandleLoadingProgress;
        model.OnLoadingFinished -= HandleLoadingFinished;
    }
    
    void RemoveAnimationListeners ()
    {
        AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    public new void Dispose ()
    {
        RemoveAnimationListeners();
        base.Dispose();
    }
}