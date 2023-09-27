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

    public static void SetupGameModel (
        IGameModel gameModel, 
        IGameStateProvider gameStateProvider, 
        MusicResource musicResource
    )
    {
        MusicFactory.SetupMusicManagerModel(
            gameModel.MusicManagerModel, 
            gameStateProvider, 
            musicResource
        );
    }
    
    public static void SetupGameNode (
        GameNode gameNode, 
        IGameModel gameModel
    ) => gameNode.Setup(gameModel.MusicManagerModel);
}