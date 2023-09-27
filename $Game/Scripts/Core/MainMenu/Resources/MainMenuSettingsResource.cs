using Godot;

[GlobalClass]
public partial class MainMenuSettingsResource : Resource
{
    [Export(PropertyHint.File, "*.json")]
    public string MainMenuSettingsJsonPath { get; set; }
}