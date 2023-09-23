using Godot;

public partial class MapWorld2DNode : Node, IMapWorld2DNode
{
    [Export]
    public PlayerController PlayerController { get; private set; }

    IPlayerModel playerModel;
    IMapInputDetectionModel mapInputDetectionModel;

    public void Setup (
        IPlayerModel playerModel, 
        IMapInputDetectionModel mapInputDetectionModel
    )
    {
        this.playerModel = playerModel;
        this.mapInputDetectionModel = mapInputDetectionModel;
    }
    
    public void Initialize ()
    {
        PlayerController.Setup(playerModel, mapInputDetectionModel);
        PlayerController.Initialize();
    }
}