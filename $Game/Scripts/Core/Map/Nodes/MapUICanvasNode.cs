using Godot;

public partial class MapUICanvasNode : Node, IMapUICanvasNode
{
    [Export] 
    NodePath PauseNodePath { get; set; }

    public IPauseNode PauseNode { get; private set; }

    IPauseModel pauseModel;
    
    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }
    public void Initialize ()
    {
        PauseNode = GetNode<IPauseNode>(PauseNodePath);
        PauseNode.Initialize();
    }
}