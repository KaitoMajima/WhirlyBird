using Godot;

public partial class MainMenuWorld2DNode : Node2D
{
    [Export] ParallaxManagerNode ParallaxManagerNode;

    public void Setup (IParallaxManagerModel parallaxManagerModel)
    {
        ParallaxManagerNode.Setup(parallaxManagerModel);
    }
    
    public void Initialize ()
    {
        
    }
    
    public new void Dispose ()
    {
        ParallaxManagerNode.Dispose();
        base.Dispose();
    }
}