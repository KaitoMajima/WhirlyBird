using Godot;

[GlobalClass]
public partial class MapSettingsResource : Resource
{
    [Export(PropertyHint.File, "*.json")]
    public string PlayerSettingsJsonPath { get; set; }
    
    [Export(PropertyHint.File, "*.json")]
    public string PillarSpawnSettingsJsonPath { get; set; }
}