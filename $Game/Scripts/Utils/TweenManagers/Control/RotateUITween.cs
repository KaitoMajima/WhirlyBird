﻿using Godot;

public partial class RotateUITween : TweenManager
{
    [Export] public float TargetRotationDegrees { get; set; }
    [Export] Control tweeningTransform;

    float CurrentRotationDegrees
    {
        get => tweeningTransform.RotationDegrees;
        set => tweeningTransform.RotationDegrees = value;
    }
    
    float originalRotationDegrees;
    
    protected override void SetupTween ()
    {
        originalRotationDegrees = CurrentRotationDegrees;
    }

    protected override void TriggerTween (bool backwards)
    {
        if (backwards)
            CurrentRotationDegrees = TargetRotationDegrees;
        
        float tweenRotationDegrees = backwards ? originalRotationDegrees : TargetRotationDegrees;
        if (TweenSettings.IsRelative)
            tweenRotationDegrees += CurrentRotationDegrees;

        float lastRotationDegrees = CurrentRotationDegrees;
        
        MainTween.TweenMethod(Callable.From<int>(progress => 
            SetRotationDegrees(lastRotationDegrees, tweenRotationDegrees, progress, TweenSettings.Amplitude)),
            lastRotationDegrees, 
            tweenRotationDegrees, 
            TweenSettings.Duration
        );
    }
    
    void SetRotationDegrees (float startValue, float endValue, float progress, float amplitude)
    {
        if (!ShouldTweenRun())
        {
            CurrentRotationDegrees = endValue;
            MainTween.Kill();
            return;
        }
        
        float value = TweenExtensions.OvershootTween(startValue, endValue, progress, amplitude);

        CurrentRotationDegrees = value;
    }
}