using System;
using Godot;

public interface ILoadingNode : IDisposable
{
    event Action OnFadeInAnimationFinished;
    event Action OnFadeOutAnimationFinished;
    
    void Initialize ();
    void FadeIn ();
    void FadeOut ();
    void SetLoadingProgress (double progress);
}