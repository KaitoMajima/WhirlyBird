using System;
using Godot;

public class PillarManagerModel : IPillarManagerModel
{
    public event Action OnPillarSpawn;
    public event Action OnPillarDifficultyChanged;
    
    public float PillarSpeed => CurrentDifficulty.PillarSpeed;
    public double PillarSecondsUntilDestruction => spawnSettings.PillarSecondsUntilDestruction;
    
    IPillarSettings CurrentDifficulty =>
        spawnSettings.PillarDifficulty[currentDifficultyIndex];
    
    readonly IPillarSpawnSettings spawnSettings;
    readonly IRandomProvider randomProvider;

    IScoreCounterModel scoreCounterModel;
    Timer currentTimer;
    int currentDifficultyIndex;
    int pillarsPassed = 100;

    public PillarManagerModel (
        IPillarSpawnSettings spawnSettings,
        IRandomProvider randomProvider
    )
    {
        this.spawnSettings = spawnSettings;
        this.randomProvider = randomProvider;
    }

    public void Setup (IScoreCounterModel scoreCounterModel)
    {
        this.scoreCounterModel = scoreCounterModel;
    }

    public void Initialize ()
    {
        AddScoreListeners();
    }
    
    public void StartTimedSpawning (Timer timer)
    {
        IPillarSettings currentDifficulty = spawnSettings.PillarDifficulty[currentDifficultyIndex];
        UpdateTimer(timer, currentDifficulty);
        GD.Print($"Pillar Speed: {currentDifficulty.PillarSpeed}, Pillar Spawn Interval: {currentDifficulty.PillarSpawnInterval}");
    }

    public Vector2 GetNewRandomSpawningPoint ()
    {
        float range = (float)randomProvider.Range(
            spawnSettings.PillarSpawnMinYHeight,
            spawnSettings.PillarSpawnMaxYHeight
        );
        Vector2 point = new(0, range);
        return point;
    }
    
    void UpdateTimer (Timer timer, IPillarSettings currentDifficulty)
    {
        RemoveTimerListeners();
        currentTimer = timer;
        currentTimer.WaitTime = currentDifficulty.PillarSpawnInterval;
        currentTimer.Start();
        AddTimerListeners();
    }

    void HandleTimerTimeout ()
    {
        OnPillarSpawn?.Invoke();
    }
    
    void HandleScoreDetected ()
    {
        pillarsPassed++;
        bool isLastDifficulty = currentDifficultyIndex >= spawnSettings.PillarDifficulty.Count - 1;
        
        if (isLastDifficulty)
            return; 
        
        IPillarSettings nextDifficulty = spawnSettings.PillarDifficulty[currentDifficultyIndex + 1];

        if (pillarsPassed < nextDifficulty.PillarPassRequirement) 
            return;
        
        currentDifficultyIndex++;
        OnPillarDifficultyChanged?.Invoke();
    }

    void AddScoreListeners ()
    {
        scoreCounterModel.OnScoreDetected += HandleScoreDetected;
    }
    
    void AddTimerListeners ()
    {
        currentTimer.Timeout += HandleTimerTimeout;
    }
    
    void RemoveScoreListeners ()
    {
        scoreCounterModel.OnScoreDetected -= HandleScoreDetected;
    }
    
    void RemoveTimerListeners ()
    {
        if (currentTimer != null)
            currentTimer.Timeout -= HandleTimerTimeout;
    }
    
    public void Dispose ()
    {
        RemoveScoreListeners();
        RemoveTimerListeners();
    }
}