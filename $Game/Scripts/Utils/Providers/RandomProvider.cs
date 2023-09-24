using Godot;

public class RandomProvider : IRandomProvider
{
    public int Range (int min, int max)
    {
        return GD.RandRange(min, max);
    }
    
    public double Range (double min, double max)
    {
        return GD.RandRange(min, max);
    }
}