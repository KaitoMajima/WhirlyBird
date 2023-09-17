using Godot;
using static GlobalSettings.Paths.Game;

public partial class GameScope : SingletonNode<GameScope>
{
    #region Resources
    ConfigResource ConfigResource => GameNode.ConfigResource;
    GameSettingsResource GameSettingsResource => GameNode.GameSettingsResource;
    #endregion

    #region Models
    public IGameSaveData GameSaveData { get; private set; }
    public IMainGameSavingSystem GameSavingSystem { get; private set; }
    #endregion
    
    #region Nodes
    public IGameNode GameNode { get; private set; }
    #endregion
    
    public override void _Ready ()
    {
        base._Ready();
        InitializeScope();
        CreateSave();
    }

    void InitializeScope ()
    {
        PackedScene gameNodeScene = GD.Load<PackedScene>(GAME_NODE_SCENE_PATH);
        GameNode gameNode = gameNodeScene.Instantiate<GameNode>();
        AddChild(gameNode);
        GameNode = gameNode;
    }

    void CreateSave ()
    {
        CryptographyResource cryptographyResource = ConfigResource.CryptographyResource;

        string cryptographyKey = cryptographyResource.CryptographyKey;
        bool useCryptographyInProduction = cryptographyResource.UseCryptographyInProduction;
        bool useCryptographyInEditor = cryptographyResource.UseCryptographyInEditor;

        ICryptographer cryptographer = GameSaveDataFactory.CreateCryptographer(
            cryptographyKey, 
            useCryptographyInProduction, 
            useCryptographyInEditor
        );
        ISerializer serializer = GameSaveDataFactory.CreateSerializer();

        GameSavingSystem = GameSaveDataFactory.CreateGameSavingSystem(cryptographer, serializer);
        GameSaveData = GameSaveDataFactory.CreateGameSaveData(GameSavingSystem);
    }
}