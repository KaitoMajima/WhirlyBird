using System;

public class ScoreCounterModel : IScoreCounterModel
{
    public event Action OnScoreDetected;

    public int Score { get; private set; }
    public int Highscore => scoreData.Highscore;

    readonly IScoreData scoreData;
    readonly IMainGameSavingSystem saveSystem;
    readonly IPlayerModel playerModel;
    readonly IGameOverModel gameOverModel;

    bool isScoreCountLocked;

    public ScoreCounterModel (
        IScoreData scoreData,
        IMainGameSavingSystem saveSystem,
        IPlayerModel playerModel, 
        IGameOverModel gameOverModel
    )
    {
        this.scoreData = scoreData;
        this.saveSystem = saveSystem;
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
        
        Score++;
        if (scoreData.Highscore < Score)
            scoreData.Highscore = Score;
        saveSystem.Save();
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