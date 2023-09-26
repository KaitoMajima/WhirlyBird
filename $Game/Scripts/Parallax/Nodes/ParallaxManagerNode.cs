using Godot;

public partial class ParallaxManagerNode : Node
{
    [Export] ParallaxBackground parallaxBackground;

    IParallaxManagerModel parallaxManagerModel;
    
    public override void _PhysicsProcess (double delta)
    {
        Vector2 scrollOffset = parallaxBackground.ScrollOffset;
        scrollOffset.X -= (float)delta * parallaxManagerModel.ParallaxOffset;
        parallaxBackground.ScrollOffset = scrollOffset;
    }
    
    public void Setup (IParallaxManagerModel parallaxManagerModel)
    {
        this.parallaxManagerModel = parallaxManagerModel;
    }
}