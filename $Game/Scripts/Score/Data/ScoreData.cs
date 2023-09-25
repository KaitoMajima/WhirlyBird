using Newtonsoft.Json;

public class ScoreData : IScoreData
{
    [JsonProperty]
    public int Highscore { get; set; }

    public ScoreData ()
    {
        
    }
    
    [JsonConstructor]
    public ScoreData (int highscore)
    {
        Highscore = highscore;
    }
}