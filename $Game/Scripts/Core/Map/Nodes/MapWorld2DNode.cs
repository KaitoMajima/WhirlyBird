using Godot;

public partial class MapWorld2DNode : Node
{
    [Export]
    public PlayerManagerNode PlayerManagerNode { get; private set; }
    
    [Export]
    public PillarManagerNode PillarManagerNode { get; private set; }
    
    [Export]
    public LevelChangeNode LevelChangeNode { get; private set; }
    
    public void Setup (
        IPlayerModel playerModel, 
        IMapInputDetectionModel mapInputDetectionModel,
        IPillarManagerModel pillarManagerModel,
        IRandomProvider randomProvider,
        ILevelChangeModel levelChangeModel
    )
    {
        PlayerManagerNode.Setup(
            playerModel, 
            mapInputDetectionModel, 
            randomProvider
        );
        PillarManagerNode.Setup(pillarManagerModel);
        LevelChangeNode.Setup(levelChangeModel);
    }
    
    public void Initialize ()
    {
        PlayerManagerNode.Initialize();
        PillarManagerNode.Initialize();
        LevelChangeNode.Initialize();
    }

    public new void Dispose ()
    {
        PlayerManagerNode.Dispose();
        PillarManagerNode.Dispose();
        LevelChangeNode.Dispose();
    }
}