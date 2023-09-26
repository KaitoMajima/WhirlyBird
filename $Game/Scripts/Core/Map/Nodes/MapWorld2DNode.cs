using Godot;

public partial class MapWorld2DNode : Node
{
    [Export]
    public PlayerManagerNode PlayerManagerNode { get; private set; }
    
    [Export]
    public PillarManagerNode PillarManagerNode { get; private set; }
    
    [Export]
    public LevelChangeNode LevelChangeNode { get; private set; }
    
    [Export]
    public LevelChangeNode LevelChangeEffectsNode { get; private set; }
    
    [Export]
    public ParallaxManagerNode ParallaxManagerNode { get; private set; }

    public void Setup (
        IPlayerModel playerModel, 
        IMapInputDetectionModel mapInputDetectionModel,
        IPillarManagerModel pillarManagerModel,
        IRandomProvider randomProvider,
        ILevelChangeModel levelChangeModel,
        IParallaxManagerModel parallaxManagerModel
    )
    {
        PlayerManagerNode.Setup(
            playerModel, 
            mapInputDetectionModel, 
            randomProvider
        );
        PillarManagerNode.Setup(pillarManagerModel);
        LevelChangeNode.Setup(levelChangeModel);
        LevelChangeEffectsNode.Setup(levelChangeModel);
        ParallaxManagerNode.Setup(parallaxManagerModel);
    }
    
    public void Initialize ()
    {
        PlayerManagerNode.Initialize();
        PillarManagerNode.Initialize();
        LevelChangeNode.Initialize();
        LevelChangeEffectsNode.Initialize();
    }

    public new void Dispose ()
    {
        PlayerManagerNode.Dispose();
        PillarManagerNode.Dispose();
        LevelChangeNode.Dispose();
        LevelChangeEffectsNode.Dispose();
        ParallaxManagerNode.Dispose();
    }
}