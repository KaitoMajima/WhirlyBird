using Godot;

[GlobalClass]
public partial class ConfigResource : Resource
{
    [Export]
    public CryptographyResource CryptographyResource { get; private set; }
}