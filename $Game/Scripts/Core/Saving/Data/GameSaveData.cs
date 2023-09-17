using Newtonsoft.Json;

namespace TapNFlap.Core.Saving.Data;

[JsonObject(MemberSerialization.OptIn)]
public class GameSaveData : IGameSaveData
{
    [JsonProperty]
    public int Highscore { get; }

    public GameSaveData ()
    {
        Highscore = 100;
    }
    
    [JsonConstructor]
    public GameSaveData (int highscore)
    {
        Highscore = highscore;
    }
}