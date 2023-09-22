using Godot;

public partial class Move2DTween : TweenManager
{
    const string PROPERTY_NAME = "position";
    
    [Export] public Vector2 TargetDestination { get; set; }
    [Export] Node2D tweeningTransform;

    Vector2 CurrentPosition
    {
        get => tweeningTransform.Position;
        set => tweeningTransform.Position = value;
    }

    Vector2 originalPosition;

    protected override void SetupTween ()
    {
        originalPosition = CurrentPosition;
    }

    protected override void TriggerTween (bool backwards)
    {
        if (backwards)
            CurrentPosition = TargetDestination;
        
        Vector2 tweenPosition = backwards ? originalPosition : TargetDestination;
        if (TweenSettings.IsRelative)
            tweenPosition += CurrentPosition;
        
        MainTween.TweenProperty(tweeningTransform, PROPERTY_NAME, tweenPosition, TweenSettings.Duration);
    }
}