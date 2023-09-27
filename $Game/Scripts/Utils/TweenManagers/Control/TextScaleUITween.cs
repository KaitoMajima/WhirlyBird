using Godot;

public partial class TextScaleUITween : TweenManager
{
    [Export] public int TargetScale { get; set; }
    [Export] Label tweeningTransform;

    int CurrentScale
    {
        get => tweeningTransform.LabelSettings.FontSize;
        set => tweeningTransform.LabelSettings.FontSize = value;
    }

    int originalScale;

    protected override void SetupTween ()
    {
        originalScale = CurrentScale;
    }

    protected override void TriggerTween (bool backwards)
    {
        if (backwards)
            CurrentScale = TargetScale;

        int tweenScale = backwards ? originalScale : TargetScale;

        if (TweenSettings.IsRelative)
            tweenScale += CurrentScale;

        int lastScale = CurrentScale;

        MainTween.TweenMethod(Callable.From<int>(progress => 
            SetScale(lastScale, tweenScale, progress, TweenSettings.Amplitude)),
            lastScale, 
            tweenScale, 
            TweenSettings.Duration
        );
    }

    void SetScale (int startValue, int endValue, int progress, float amplitude)
    {
        if (!ShouldTweenRun())
        {
            CurrentScale = ClampProgressValue(endValue);
            MainTween.Kill();
            return;
        }
        
        int value = Mathf.RoundToInt(
            TweenExtensions.OvershootTween(
                startValue, 
                endValue, 
                progress, 
                amplitude
            )
        );
        
        CurrentScale = ClampProgressValue(value);
    }

    int ClampProgressValue (int value) 
        => Mathf.Clamp(value, 1, 4096);
}