using Godot;

public partial class FadeTween : TweenManager
{
    [Export(PropertyHint.Range, "0,1,0.05")]
    public float TargetAlpha { get; set; }
    
    [Export] CanvasItem tweeningTransform;
    [Export] bool setMouseInteractableOnFinish;

    float CurrentAlpha
    {
        get => tweeningTransform.Modulate.A;
        set
        {
            currentColor = new Color(
                tweeningTransform.Modulate.R,
                tweeningTransform.Modulate.G, 
                tweeningTransform.Modulate.B,
                value
            );
            tweeningTransform.Modulate = currentColor;
        }
    }

    Color currentColor;

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
        if (MainTween.GetTotalElapsedTime() >= TweenSettings.Duration + TweenSettings.Delay)
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