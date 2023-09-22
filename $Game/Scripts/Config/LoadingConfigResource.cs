using Godot;

[GlobalClass]
public partial class LoadingConfigResource : Resource
{
    [Export] 
    public bool UseMultiThreadedLoading;
}