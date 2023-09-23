using Godot;

public partial class MapWorld2DNode : Node
{
    [Export]
    public PlayerControllerNode PlayerControllerNode { get; private set; }
    
    [Export]
    public PillarManagerNode PillarManagerNode { get; private set; }

    IPlayerModel playerModel;
    IMapInputDetectionModel mapInputDetectionModel;
    IPillarManagerModel pillarManagerModel;

    public void Setup (
        IPlayerModel playerModel, 
        IMapInputDetectionModel mapInputDetectionModel,
        IPillarManagerModel pillarManagerModel
    )
    {
        this.playerModel = playerModel;
        this.mapInputDetectionModel = mapInputDetectionModel;
        this.pillarManagerModel = pillarManagerModel;
    }
    
    public void Initialize ()
    {
        PlayerControllerNode.Setup(playerModel, mapInputDetectionModel);
        PlayerControllerNode.Initialize();
        
        PillarManagerNode.Setup(pillarManagerModel);
        PillarManagerNode.Initialize();
    }
}