using Godot;

public static class NodeExtensions
{
    public static void SetActive (this Node node, bool value)
    {
        node.SetProcess(value);
        node.SetPhysicsProcess(value);
        node.SetBlockSignals(value);
        node.SetProcessInput(value);
    }
    
    public static void SetActive (this CanvasItem node, bool value)
    {
        SetActive((Node)node, value);
        node.Visible = value;
    }
    
    public static void SetActive (this Node3D node, bool value)
    {
        SetActive((Node)node, value);
        node.Visible = value;
    }
}