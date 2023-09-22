using Godot;
using static Godot.Tween;

[GlobalClass]
public partial class TweenSettingsResource : Resource
{
    [Export]
    public InitMethod InitializeMethod;
    [Export]
    public DisposeMethod DisposingMethod = DisposeMethod.ExitTree;

    [Export]
    public TweenDirection Direction;
    
    [Export]
    public TweenProcessMode ProcessMode;

    [Export]
    public float TweenTimeScale = 1;
    
    [Export]
    public int LoopAmount;
    
    [Export]
    public float Duration = 1;
    [Export]
    public float Delay;

    [Export] 
    public bool IsRelative;
    
    [Export]
    public EaseType EaseType = EaseType.Out;
    [Export]
    public TransitionType TransitionType = TransitionType.Cubic;
    
    public void ApplyTweenSettings (Tween tween)
    {
        tween.SetEase(EaseType).SetTrans(TransitionType).SetLoops(LoopAmount)
            .SetSpeedScale(TweenTimeScale).SetProcessMode(ProcessMode);

        tween.TweenInterval(Delay);
    }
    
    public enum InitMethod
    {
        None,
        EnterTree,
        Ready
    }

    public enum DisposeMethod
    {
        Manual,
        ExitTree
    }

    public enum TweenDirection
    {
        Forwards,
        Backwards
    }
}