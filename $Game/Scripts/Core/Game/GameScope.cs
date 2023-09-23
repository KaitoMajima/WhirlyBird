public partial class GameScope : SingletonNode
{
    public static GameScope Instance { get; private set; }
    
    public ITimeProvider TimeProvider { get; private set; }
    
    public IGameSaveData GameSaveData { get; private set; }
    public IMainGameSavingSystem GameSavingSystem { get; private set; }
    
    public GameNode GameNode { get; private set; }
    
    ConfigResource ConfigResource => GameNode.ConfigResource;
    
    public override void _Ready ()
    {
        base._Ready();
        Instance = RegisterSingletonInstance(this);
        SetupProviders();
        SetupNodes();
        SetupSave();
    }

    void SetupProviders ()
    {
        TimeProvider = new TimeProvider();
    }

    void SetupNodes ()
    {
        GameNode = GameFactory.CreateGameNode(this);
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
}