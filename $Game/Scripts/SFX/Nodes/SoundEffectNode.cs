using Godot;

public partial class SoundEffectNode : AudioStreamPlayer
{
    [Export] public SoundEffectResource SoundEffectResource { get; private set; }

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