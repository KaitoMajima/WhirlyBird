using Godot;

public static class TweenExtensions
{
    public static float OvershootTween (float startValue, float endValue, float progress, float amplitude)
    {
        if (amplitude == 0)
            return progress;
        
        float weight = progress / endValue;
        float overshotValue = weight - amplitude * Mathf.Sin(weight * Mathf.Pi * 2);
        float value = (startValue + (endValue - startValue)) * overshotValue;

        return value;
    }
}