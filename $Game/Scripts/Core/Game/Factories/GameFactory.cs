using Godot;
using static GlobalSettings.Paths.Game;

public static class GameFactory
{
    public static IGameNode CreateGameNode (Node callerNode)
    {
        PackedScene gameNodeScene = GD.Load<PackedScene>(GAME_NODE_SCENE_PATH);
        GameNode gameNode = gameNodeScene.Instantiate<GameNode>();
        callerNode.AddChild(gameNode);
        return gameNode;
    }
}