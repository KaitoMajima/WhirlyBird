using Godot;

public class TimeProvider : ITimeProvider
{
    public double TimeScale
    {
        get => Engine.TimeScale;
        set => Engine.TimeScale = value;
    }
}