using System;

public class ScoreCounterModel : IScoreCounterModel
{
    public event Action OnScoreDetected;

    public int Score { get; private set; }
    // public int Highscore => scoreData.Highscore;
    
    readonly IPlayerModel playerModel;
        
    public ScoreCounterModel (IPlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }

    public void Initialize ()
    {
        AddModelListeners();
    }

    void AddModelListeners ()
    {
        playerModel.OnPlayerScored += HandlePlayerScored;
    }
        
    void RemoveModelListeners ()
    {
        playerModel.OnPlayerScored -= HandlePlayerScored;
    }

    void HandlePlayerScored ()
    {
        // if (scoreData.Highscore < Score)
        //     scoreData.Highscore = Score;
        Score++;
        OnScoreDetected?.Invoke();
    }

    public void Dispose ()
    {
        RemoveModelListeners();
    }
}