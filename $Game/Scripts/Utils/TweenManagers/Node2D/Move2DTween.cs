using Godot;

public partial class Move2DTween : TweenManager
{
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

        Vector2 lastPosition = CurrentPosition;
        
        MainTween.TweenMethod(Callable.From<Vector2>(progress => 
            SetPosition(lastPosition, tweenPosition, progress, TweenSettings.Amplitude)),
            lastPosition,
            tweenPosition,
            TweenSettings.Duration
        );
    }
    
    void SetPosition (Vector2 startValue, Vector2 endValue, Vector2 progress, float amplitude)
    {
        if (MainTween.GetTotalElapsedTime() >= TweenSettings.Duration + TweenSettings.Delay)
        {
            CurrentPosition = endValue;
            MainTween.Kill();
            return;
        }
        
        float xValue = TweenExtensions.OvershootTween(startValue.X, endValue.X, progress.X, amplitude);
        float yValue = TweenExtensions.OvershootTween(startValue.Y, endValue.Y, progress.Y, amplitude);

        CurrentPosition = new Vector2(xValue, yValue);
    }
}