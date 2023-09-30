using System;

public class PillarManagerModel : IPillarManagerModel
{
    public event Action OnPillarSpawn;
    public event Action OnPillarPassed;
    public event Action OnPillarDifficultyChanged;

    public int PillarsPassedCount { get; private set; }
    
    public float PillarSpeed => CurrentDifficulty.PillarSpeed;
    public double PillarSecondsUntilDestruction => spawnSettings.PillarSecondsUntilDestruction;
    public float ParallaxBaseValue => spawnSettings.ParallaxBaseValue;
    public float ParallaxMultiplier => CurrentDifficulty.ParallaxMultiplier;

    IPillarSettings CurrentDifficulty =>
        spawnSettings.PillarDifficulty[currentDifficultyIndex];
    
    readonly IPillarSpawnSettings spawnSettings;
    readonly IRandomProvider randomProvider;

    IScoreCounterModel scoreCounterModel;
    ITimer timer;
    int currentDifficultyIndex;

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

    public void SetTimer (ITimer timer)
    {
        this.timer = timer;
        AddTimerListeners();
    }

    public void Initialize ()
    {
        PillarsPassedCount = spawnSettings.InitialPillarsPassed;
        AddScoreListeners();
    }

    public void StartSpawning ()
    {
        timer.Stop();
        timer.WaitTime = CurrentDifficulty.PillarSpawnInterval;
        timer.Start();
    }

    public float GetNewRandomSpawningPoint ()
    {
        return (float)randomProvider.Range(
            spawnSettings.PillarSpawnMinYHeight,
            spawnSettings.PillarSpawnMaxYHeight
        );
    }

    void HandleTimerTimeout ()
    {
        OnPillarSpawn?.Invoke();
    }
    
    void HandleScoreDetected ()
    {
        PillarsPassedCount++;
        OnPillarPassed?.Invoke();
        bool isLastDifficulty = currentDifficultyIndex >= spawnSettings.PillarDifficulty.Count - 1;
        
        if (isLastDifficulty)
            return; 
        
        IPillarSettings nextDifficulty = spawnSettings.PillarDifficulty[currentDifficultyIndex + 1];

        if (PillarsPassedCount < nextDifficulty.PillarPassRequirement) 
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
        timer.Timeout += HandleTimerTimeout;
    }
    
    void RemoveScoreListeners ()
    {
        scoreCounterModel.OnScoreDetected -= HandleScoreDetected;
    }
    
    void RemoveTimerListeners ()
    {
        if (!timer.IsStopped())
            timer.Timeout -= HandleTimerTimeout;
    }
    
    public void Dispose ()
    {
        RemoveScoreListeners();
        RemoveTimerListeners();
    }
}