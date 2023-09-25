using Godot;
using static GlobalSettings.Paths.Game;

public static class GameFactory
{
    public static IGameModel CreateGameModel ()
    {
        IMusicManagerModel musicManagerModel = MusicFactory.CreateMusicManagerModel();
        return new GameModel(musicManagerModel);
    }
    
    public static GameNode CreateGameNode (Node callerNode)
    {
        PackedScene gameNodeScene = GD.Load<PackedScene>(GAME_NODE_SCENE_PATH);
        GameNode gameNode = gameNodeScene.Instantiate<GameNode>();
        callerNode.AddChild(gameNode);
        return gameNode;
    }

    public static void SetupGameModel (IGameModel gameModel, MusicResource musicResource)
    {
        MusicFactory.SetupMusicManagerModel(gameModel.MusicManagerModel, musicResource);
    }
}