using Godot;

public partial class MapWorld2DNode : Node
{
    [Export]
    public PlayerManagerNode PlayerManagerNode { get; private set; }
    
    [Export]
    public PillarManagerNode PillarManagerNode { get; private set; }
    
    public void Setup (
        IPlayerModel playerModel, 
        IMapInputDetectionModel mapInputDetectionModel,
        IPillarManagerModel pillarManagerModel,
        IRandomProvider randomProvider
    )
    {
        PlayerManagerNode.Setup(
            playerModel, 
            mapInputDetectionModel, 
            randomProvider
        );
        PillarManagerNode.Setup(pillarManagerModel);
    }
    
    public void Initialize ()
    {
        PlayerManagerNode.Initialize();
        PillarManagerNode.Initialize();
    }
}