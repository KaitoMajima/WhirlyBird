using Godot;
using TapNFlap.Core.Saving.Cryptography;
using TapNFlap.Core.Saving.Data;
using TapNFlap.Core.Saving.Serialization;

namespace TapNFlap.Core.Saving.Factories;

public static class GameSaveDataFactory
{
    const string EDITOR_GAME_SAVE_DIRECTORY = "EditorSaves";
    const string GAME_SAVE_DIRECTORY = "Saves";
    const string GAME_SAVE_NAME = "playerSave";
    const string GAME_SAVE_FORMAT = ".sav";

    static string UserDataDirectory => OS.GetUserDataDir();
    static bool IsGameRunningInEditor => OS.HasFeature("editor");
    
    public static ICryptographer CreateCryptographer (
        string key, 
        bool useCryptographyInProduction = true, 
        bool useCryptographyInEditor = false
    )
    {
        ICryptographer cryptographer;

        bool cryptographyActiveInEditor = IsGameRunningInEditor && useCryptographyInEditor;
        bool cryptographyActiveInProduction = !IsGameRunningInEditor && useCryptographyInProduction;

        bool isCryptographyActive = cryptographyActiveInEditor || cryptographyActiveInProduction;
        
        cryptographer = isCryptographyActive
            ? new AESCryptographer()
            : new NullCryptographer();

        cryptographer.Key = key;
        
        return cryptographer;
    }

    public static ISerializer CreateSerializer () 
        => new JsonSerializer();

    public static IMainGameSavingSystem CreateGameSavingSystem (
        ICryptographer cryptographer, 
        ISerializer serializer
    )
    {
        MainGameSavingSystem gameSavingSystem = new(cryptographer, serializer);
        gameSavingSystem.Setup(
            UserDataDirectory,
            IsGameRunningInEditor ? EDITOR_GAME_SAVE_DIRECTORY : GAME_SAVE_DIRECTORY,
            GAME_SAVE_NAME, 
            GAME_SAVE_FORMAT
        );
        return gameSavingSystem;
    }

    public static IGameSaveData CreateGameSaveData (IMainGameSavingSystem gameSavingSystem)
    {
        bool isBrandNewSave = !gameSavingSystem.SaveExists();
        IGameSaveData gameSaveData;
        
        if (isBrandNewSave)
        {
            gameSaveData = new GameSaveData();
            gameSavingSystem.CreateNewSave(gameSaveData);
        }
        else
        {
            gameSaveData = gameSavingSystem.Load();
        }

        return gameSaveData;
    }
}