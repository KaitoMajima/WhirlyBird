using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameSaveData : IGameSaveData
{
    [JsonProperty]
    public IScoreData ScoreData { get; }

    public GameSaveData ()
    {
        ScoreData = new ScoreData();
    }
    
    [JsonConstructor]
    public GameSaveData (ScoreData scoreData)
    {
        ScoreData = scoreData ?? new ScoreData();
    }
}