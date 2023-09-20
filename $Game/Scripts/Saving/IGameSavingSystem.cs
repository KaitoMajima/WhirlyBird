public interface IGameSavingSystem<TSave, TUSave> where TUSave : TSave
{
    void Setup (
        string userDataDirectoryName,
        string directoryName,
        string saveFileName,
        string saveFileFormat
    );
    void CreateNewSave (TSave save);
    void Save ();
    TSave Load ();
    bool SaveExists ();
}