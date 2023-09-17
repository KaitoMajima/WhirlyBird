using System.IO;

public abstract class BaseGameSavingSystem<TSave, TUSave> : IGameSavingSystem<TSave, TUSave> where TUSave : TSave
{
    string GameSaveDirectoryPath => Path.Join(userDataDirectory, gameSaveDirectory);
    string GameSavePath => Path.Join(GameSaveDirectoryPath, gameSaveName + gameSaveFormat);
    
    readonly ICryptographer cryptographer;
    readonly ISerializer serializer;

    string userDataDirectory;
    string gameSaveDirectory;
    string gameSaveName;
    string gameSaveFormat;
    
    TSave loadedGameSave;

    public BaseGameSavingSystem (
        ICryptographer cryptographer, 
        ISerializer serializer
    )
    {
        this.cryptographer = cryptographer;
        this.serializer = serializer;
    }

    public void Setup (
        string userDataDirectoryName, 
        string directoryName, 
        string saveFileName, 
        string saveFileFormat
    )
    {
        userDataDirectory = userDataDirectoryName;
        gameSaveDirectory = directoryName;
        gameSaveName = saveFileName;
        gameSaveFormat = saveFileFormat;
    }

    public void CreateNewSave (TSave save)
    {
        loadedGameSave = save;
        Save();
    }

    public void Save ()
    {
        if (!Directory.Exists(GameSaveDirectoryPath))
            Directory.CreateDirectory(GameSaveDirectoryPath);

        string rawSave = serializer.Serialize(loadedGameSave);
        rawSave = cryptographer.Encrypt(rawSave);
        
        File.WriteAllText(GameSavePath, rawSave);
    }

    public TSave Load ()
    {
        if (!SaveExists())
            throw new FileNotFoundException(
                "Could not find a save file in the specified path! " +
                "Make sure to load only after a save has been created."
            );
        
        string rawSave = File.ReadAllText(GameSavePath);
        rawSave = cryptographer.Decrypt(rawSave);
        
        loadedGameSave = serializer.Deserialize<TUSave>(rawSave);
        return loadedGameSave;
    }

    public bool SaveExists () => File.Exists(GameSavePath);
}