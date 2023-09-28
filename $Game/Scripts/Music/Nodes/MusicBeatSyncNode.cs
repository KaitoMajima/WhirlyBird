using Godot;

public partial class MusicBeatSyncNode : Node2D
{
    const int MIN_HZ = 0;
    const int MAX_HZ = 20000;
    
    [Export] Node2D targetTransform;
    [Export] float scaleMultiplier = 1;
    [Export] float lerpSpeed = 0.1f;
    [Export] string audioBusName = "Master";
    [Export] int spectrumEffectIndex;
    
    AudioEffectSpectrumAnalyzerInstance spectrumInstance;
    AudioStreamPlayer audioStreamPlayer;
    
    Vector2 currentScale = new(1, 1);
    
    public override void _Ready ()
    {
        audioStreamPlayer = GameScope.Instance.GameNode.MusicManagerNode.MainMusicPlayer;
        int busIndex = AudioServer.GetBusIndex(audioBusName);
        spectrumInstance = (AudioEffectSpectrumAnalyzerInstance)AudioServer.GetBusEffectInstance(busIndex, spectrumEffectIndex);
    }

    public override void _PhysicsProcess (double delta)
    {
        if (!IsVisibleInTree())
            return;
        
        Vector2 magnitudes = spectrumInstance.GetMagnitudeForFrequencyRange(
            MIN_HZ, 
            MAX_HZ, 
            AudioEffectSpectrumAnalyzerInstance.MagnitudeMode.Average
        ); 
            
        float averageMagnitude = (magnitudes.X + magnitudes.Y) / 2.0f;
        
        Vector2 targetScale = new(1 + averageMagnitude * scaleMultiplier, 1 + averageMagnitude * scaleMultiplier);
        currentScale = currentScale.Lerp(targetScale, lerpSpeed * (float)delta);
        targetTransform.Scale = currentScale;
    }
}