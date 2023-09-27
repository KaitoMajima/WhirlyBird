using Godot;

public partial class FadeLight2DTween : TweenManager
{
    [Export(PropertyHint.Range, "0,1,0.05")]
    public float TargetAlpha { get; set; }
    
    [Export] Light2D tweeningTransform;

    float CurrentAlpha
    {
        get => tweeningTransform.Energy;
        set => tweeningTransform.Energy = value;
    }
    
    float originalAlpha;

    protected override void SetupTween ()
    {
        originalAlpha = CurrentAlpha;
    }

    protected override void TriggerTween (bool backwards)
    {
        if (backwards)
            CurrentAlpha = TargetAlpha;

        float tweenAlpha = backwards ? originalAlpha : TargetAlpha;

        if (TweenSettings.IsRelative)
            tweenAlpha += CurrentAlpha;

        float lastAlpha = CurrentAlpha;
        
        MainTween.TweenMethod(Callable.From<float>(progress => 
            SetAlpha(lastAlpha, tweenAlpha, progress, TweenSettings.Amplitude)),
            lastAlpha, 
            tweenAlpha, 
            TweenSettings.Duration
        );
    }

    void SetAlpha (float startValue, float endValue, float progress, float amplitude)
    {
        if (!ShouldTweenRun())
        {
            CurrentAlpha = ClampProgressValue(endValue);
            MainTween.Kill();
            return;
        }

        float value = TweenExtensions.OvershootTween(
            startValue,
            endValue,
            progress,
            amplitude
        );
        
        CurrentAlpha = ClampProgressValue(value);
    }

    float ClampProgressValue (float value) 
        => Mathf.Clamp(value, 0, 1);
}