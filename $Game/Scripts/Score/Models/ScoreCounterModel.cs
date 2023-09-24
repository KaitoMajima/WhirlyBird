using System;

public class ScoreCounterModel : IScoreCounterModel
{
    public event Action OnScoreDetected;

    public int Score { get; private set; }
    // public int Highscore => scoreData.Highscore;
    
    readonly IPlayerModel playerModel;
    readonly IGameOverModel gameOverModel;

    bool isScoreCountLocked;

    public ScoreCounterModel (
        IPlayerModel playerModel, 
        IGameOverModel gameOverModel
    )
    {
        this.playerModel = playerModel;
        this.gameOverModel = gameOverModel;
    }

    public void Initialize ()
    {
        AddModelListeners();
    }
    
    void HandleGameOverTriggered ()
    {
        isScoreCountLocked = true;
    }
    
    void HandlePlayerScored ()
    {
        if (isScoreCountLocked)
            return;
        
        // if (scoreData.Highscore < Score)
        //     scoreData.Highscore = Score;
        Score++;
        OnScoreDetected?.Invoke();
    }

    void AddModelListeners ()
    {
        playerModel.OnPlayerScored += HandlePlayerScored;
        gameOverModel.OnGameOverTriggered += HandleGameOverTriggered;
    }

    void RemoveModelListeners ()
    {
        playerModel.OnPlayerScored -= HandlePlayerScored;
        gameOverModel.OnGameOverTriggered -= HandleGameOverTriggered;
    }
    
    public void Dispose ()
    {
        RemoveModelListeners();
    }
}