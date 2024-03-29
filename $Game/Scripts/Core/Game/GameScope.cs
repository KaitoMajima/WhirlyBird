﻿public partial class GameScope : SingletonNode
{
    public static GameScope Instance { get; private set; }
    
    public ITimeProvider TimeProvider { get; private set; }
    public IRandomProvider RandomProvider { get; private set; }
    public IGameStateProvider GameStateProvider { get; private set; }
    
    public IGameSaveData GameSaveData { get; private set; }
    public IMainGameSavingSystem GameSavingSystem { get; private set; }
    
    public IGameModel GameModel { get; private set; }
    public GameNode GameNode { get; private set; }
    
    ConfigResource ConfigResource => GameNode.ConfigResource;
    MusicResource MusicResource => GameNode.MusicResource;
    
    public override void _Ready ()
    {
        base._Ready();
        Instance = RegisterSingletonInstance(this);
        SetupProviders();
        SetupModels();
        SetupNodes();
        SetupSave();
        InitializeModels();
        InitializeNodes();
    }

    public override void _ExitTree ()
    {
        DisposeModels();
        DisposeNodes();
    }
    
    void SetupProviders ()
    {
        TimeProvider = new TimeProvider();
        RandomProvider = new RandomProvider();
        GameStateProvider = new GameStateProvider();
    }
    
    void SetupModels ()
    {
        GameModel = GameFactory.CreateGameModel();
    }

    void SetupNodes ()
    {
        GameNode = GameFactory.CreateGameNode(this);
        GameFactory.SetupGameNode(GameNode, GameModel);
        GameFactory.SetupGameModel(GameModel, GameStateProvider, MusicResource);
    }
    
    void SetupSave ()
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
    
    void InitializeModels ()
    {
        GameModel.Initialize();
    }
    
    void InitializeNodes ()
    {
        GameNode.Initialize();
    }
    
    void DisposeModels ()
    {
        GameModel.Dispose();
    }
    
    void DisposeNodes ()
    {
        GameNode.Dispose();
    }
}