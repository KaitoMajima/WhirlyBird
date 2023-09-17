using Godot;
using Newtonsoft.Json;
using TapNFlap.Core.Config;
using TapNFlap.Core.Game.Nodes;
using TapNFlap.Core.Game.Resources;
using TapNFlap.Core.Saving;
using TapNFlap.Core.Saving.Cryptography;
using TapNFlap.Core.Saving.Data;
using TapNFlap.Core.Saving.Factories;
using TapNFlap.Core.Saving.Serialization;
using TapNFlap.Core.Utils;
using static TapNFlap.Core.Utils.GlobalSettings.Paths.Game;

namespace TapNFlap.Core.Game;

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