using Godot;

namespace TapNFlap.Core.Config;

[GlobalClass]
public partial class CryptographyResource : Resource
{
    [Export]
    public bool UseCryptographyInProduction = true;
    [Export]
    public bool UseCryptographyInEditor;
    [Export]
    public string CryptographyKey;
}