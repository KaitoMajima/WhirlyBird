using Godot;

public partial class PauseNode : Node, IPauseNode
{
    [Export]
    NodePath CenterButtonsPath { get; set; }
    
    public PauseMenuCenterButtons CenterButtons { get; private set; }

    IPauseModel pauseModel;

    public void Setup (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }

    public void Initialize ()
    {
        CenterButtons = GetNode<PauseMenuCenterButtons>(CenterButtonsPath);
        CenterButtons.Initialize();
    }
    
    public new void Dispose ()
    {
        CenterButtons.Dispose();
        base.Dispose();
    }
}