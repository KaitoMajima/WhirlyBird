using Godot;

public static class RangeConverter
{
    public static float ConvertValues(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        float normalizedValue = Mathf.InverseLerp(inputMin, inputMax, value);
        float resultValue = Mathf.Lerp(outputMin, outputMax, normalizedValue);

        return resultValue;
    }
}