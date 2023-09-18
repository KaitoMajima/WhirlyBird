using System;
using Godot;

public partial class LoadingNode : Node, ILoadingNode
{
    public event Action OnFadeInAnimationFinished;
    public event Action OnFadeOutAnimationFinished;
    
    [Export]
    NodePath AnimationPlayerPath { get; set; }
    
    [Export]
    NodePath ProgressBarPath { get; set; }

    [Export] 
    StringName FadeInAnimation { get; set; }
    
    [Export] 
    StringName FadeOutAnimation { get; set; }

    AnimationPlayer animationPlayer;
    ProgressBar progressBar;
    
    public void Initialize ()
    {
        animationPlayer = GetNode<AnimationPlayer>(AnimationPlayerPath);
        progressBar = GetNode<ProgressBar>(ProgressBarPath);
        AddAnimationListeners();
    }
    
    public void FadeIn ()
    {
        animationPlayer.Play(FadeInAnimation);
    }
    
    public void FadeOut ()
    {
        animationPlayer.Play(FadeOutAnimation);
    }

    public void SetLoadingProgress (double progress)
    {
        progressBar.Value = progress;
    }

    void AddAnimationListeners ()
    {
        animationPlayer.AnimationFinished += HandleAnimationFinished;
    }
    
    void RemoveAnimationListeners ()
    {
        animationPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    void HandleAnimationFinished (StringName animationName)
    {
        if (animationName == FadeInAnimation)
            OnFadeInAnimationFinished?.Invoke();
        else if (animationName == FadeOutAnimation)
            OnFadeOutAnimationFinished?.Invoke();
    }

    public new void Dispose ()
    {
        RemoveAnimationListeners();
        base.Dispose();
    }
}