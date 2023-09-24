using Godot;

public partial class ScaleUITween : TweenManager
{
    [Export] public Vector2 TargetScale { get; set; }
    [Export] Control tweeningTransform;

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

        Vector2 lastScale = CurrentScale;

        MainTween.TweenMethod(Callable.From<Vector2>(progress =>
            SetScale(lastScale, tweenScale, progress, TweenSettings.Amplitude)),
            lastScale,
            tweenScale,
            TweenSettings.Duration
        );
    }
    
    void SetScale (Vector2 startValue, Vector2 endValue, Vector2 progress, float amplitude)
    {
        if (MainTween.GetTotalElapsedTime() >= TweenSettings.Duration)
        {
            CurrentScale = endValue;
            MainTween.Kill();
        }
        
        float xValue = TweenExtensions.OvershootTween(startValue.X, endValue.X, progress.X, amplitude);
        float yValue = TweenExtensions.OvershootTween(startValue.Y, endValue.Y, progress.Y, amplitude);

        CurrentScale = new Vector2(xValue, yValue);
    }
}