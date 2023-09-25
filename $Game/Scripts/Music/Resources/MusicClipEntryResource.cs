using Godot;

public partial class MusicClipEntryResource : Resource
{
    [Export]
    public MusicClipType MusicClipType;
    
    [Export]
    public AudioStream AudioStream;
    
    [Export]
    public float CrossfadeTimeOnPlay = 1;
    
    [Export]
    public float MaxVolume = 0.5f;
    
    [Export]
    public float Offset;
}