using Godot;

[GlobalClass]
public partial class MusicBeatSyncResource : Resource
{
    [Export] public float UpdateStep { get; private set; } = 0.025f;
    [Export] public int SampleDataLength { get; private set; } = 1024;
    [Export] public float ValueFactor { get; private set; } = 20;
    [Export] public float LimitLoudness { get; private set; } = 30;
    [Export] public float MinScaleValue { get; private set; } = 4;
    [Export] public float MaxScaleValue { get; private set; } = 8;
}