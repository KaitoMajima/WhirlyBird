using Godot;

public partial class MapWorld2DNode : Node, IMapWorld2DNode
{
    [Export] 
    NodePath PlayerControllerPath { get; set; }

    public PlayerController PlayerController { get; private set; }

    IPlayerModel playerModel;

    public void Setup (IPlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }
    
    public void Initialize ()
    {
        PlayerController = GetNode<PlayerController>(PlayerControllerPath);
        PlayerController.Setup(playerModel);
        PlayerController.Initialize();
    }
}