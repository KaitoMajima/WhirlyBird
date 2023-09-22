using Godot;

public partial class Rotate2DTween : TweenManager
{
    const string PROPERTY_NAME = "rotation_degrees";
    
    [Export] public float TargetRotationDegrees { get; set; }
    [Export] Node2D tweeningTransform;

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
        
        MainTween.TweenProperty(tweeningTransform, PROPERTY_NAME, tweenRotationDegrees, TweenSettings.Duration);
    }
}