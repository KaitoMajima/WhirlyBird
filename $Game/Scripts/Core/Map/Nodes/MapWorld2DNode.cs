using Godot;

public partial class MapWorld2DNode : Node, IMapWorld2DNode
{
    [Export]
    public PlayerController PlayerController { get; private set; }
    
    [Export]
    PillarManagerNode PillarManagerNodeInstance { get; set; }
    
    public IPillarManagerNode PillarManagerNode { get; private set; }

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
        PlayerController.Setup(playerModel, mapInputDetectionModel);
        PlayerController.Initialize();
        
        PillarManagerNode = PillarManagerNodeInstance;
        PillarManagerNode.Setup(pillarManagerModel);
        PillarManagerNode.Initialize();
    }
}