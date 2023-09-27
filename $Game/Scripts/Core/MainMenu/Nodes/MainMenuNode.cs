using Godot;

public partial class MainMenuNode : Node
{
    [Export] 
    public MainMenuCenterButtons MainMenuCenterButtons { get; private set; }

    [Export] 
    public ParallaxManagerNode ParallaxManagerNode { get; private set; }

    public void Setup (IParallaxManagerModel parallaxManagerModel)
    {
        ParallaxManagerNode.Setup(parallaxManagerModel);
    }
    
    public void Initialize ()
    {
        MainMenuCenterButtons.Initialize();
    }

    public new void Dispose ()
    {
        MainMenuCenterButtons.Dispose();
        ParallaxManagerNode.Dispose();
        base.Dispose();
    }
}