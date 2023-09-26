using Godot;

[GlobalClass]
public partial class SoundEffectResource : Resource
{
    [Export]
    public bool UsePitchVariation;
    
    [Export]
    public float MinPitchVariation = 0.8f;
    
    [Export]
    public float MaxPitchVariation = 1.2f;
}