using Godot;

public partial class SoundEffectNode : AudioStreamPlayer
{
    [Export] public string BusName = "SFX";
    [Export] public SoundEffectResource SoundEffectResource { get; private set; }

    public override void _Ready ()
    {
        Bus = BusName;
    }

    public void PlaySFX ()
    {
        if (SoundEffectResource.UsePitchVariation)
            PitchScale = (float)GD.RandRange(
                SoundEffectResource.MinPitchVariation,
                SoundEffectResource.MaxPitchVariation
            );
        
        Play();
    }
}