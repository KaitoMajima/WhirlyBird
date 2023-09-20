using Godot;

public partial class MapWorld2DNode : Node, IMapWorld2DNode
{
    [Export] 
    NodePath playerControllerPath;

    public PlayerController PlayerController { get; private set; }

    IPlayerModel playerModel;

    public void Setup (IPlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }
    
    public void Initialize ()
    {
        PlayerController = GetNode<PlayerController>(playerControllerPath);
        PlayerController.Setup(playerModel);
        PlayerController.Initialize();
    }
}