using Godot;

public partial class Scale2DTween : TweenManager
{
    const string PROPERTY_NAME = "scale";
    
    [Export] public Vector2 TargetScale { get; set; }
    [Export] Node2D tweeningTransform;

    Vector2 CurrentScale
    {
        get => tweeningTransform.Scale;
        set => tweeningTransform.Scale = value;
    }
    
    Vector2 originalScale;

    protected override void SetupTween ()
    {
        originalScale = CurrentScale;
    }

    protected override void TriggerTween (bool backwards)
    {
        if (backwards)
            CurrentScale = TargetScale;
        
        Vector2 tweenScale = backwards ? originalScale : TargetScale;
        
        if (TweenSettings.IsRelative)
            tweenScale += CurrentScale;
        
        MainTween.TweenProperty(tweeningTransform, PROPERTY_NAME, tweenScale, TweenSettings.Duration);
    }
}