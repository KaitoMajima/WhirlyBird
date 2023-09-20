using Godot;

public static class MainMenuFactory
{
    public static IMainMenuNode CreateMainMenuNode (
        Node callerNode,
        NodePath mainMenuNodePath
    )
    {
        IMainMenuNode mainMenuNode = callerNode.GetNode<MainMenuNode>(mainMenuNodePath);
        return mainMenuNode;
    }
}