using Godot;

public abstract partial class TweenManager : Node
{
    [Export] 
    public TweenSettingsResource TweenSettings { get; private set; }

    [Export] 
    public bool KillLastTween;

    public Tween MainTween { get; private set; }

    #region Initialize Methods

    public override void _Ready ()
    {
        SetupTween();
        if (TweenSettings.InitializeMethod == TweenSettingsResource.InitMethod.Ready)
            PlayTween();
    }
    
    public override void _EnterTree ()
    {
        if (TweenSettings.InitializeMethod == TweenSettingsResource.InitMethod.EnterTree)
            PlayTween();
    }

    public override void _ExitTree ()
    {
        if (TweenSettings.DisposingMethod == TweenSettingsResource.DisposeMethod.ExitTree)
            Dispose();
    }
    
    #endregion

    public virtual void PlayTween ()
    {
        if (KillLastTween)
            StopTween();
        
        MainTween = CreateTween();
        TweenSettings.ApplyTweenSettings(MainTween);
        TriggerTween(TweenSettings.Direction == TweenSettingsResource.TweenDirection.Backwards);
    }

    public virtual void StopTween ()
    {
        MainTween?.Kill();
    }

    public new void Dispose ()
    {
        StopTween();
        base.Dispose();
    }
    
    protected abstract void SetupTween ();
    protected abstract void TriggerTween (bool isBackwards);
}