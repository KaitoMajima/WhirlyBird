using System;
using Godot;

public class PillarManagerModel : IPillarManagerModel
{
    public event Action OnPillarSpawn;
    
    public int PillarId => CurrentDifficulty.PillarId;
    public float PillarSpeed => CurrentDifficulty.PillarSpeed;
    public double PillarSecondsUntilDestruction => pillarSpawnSettings.PillarSecondsUntilDestruction;
    
    IPillarSettings CurrentDifficulty =>
        pillarSpawnSettings.PillarDifficulty[currentDifficultyIndex];
    
    readonly IPillarSpawnSettings pillarSpawnSettings;
    readonly IRandomProvider randomProvider;

    Timer currentTimer;
    int currentDifficultyIndex;

    public PillarManagerModel (
        IPillarSpawnSettings pillarSpawnSettings,
        IRandomProvider randomProvider
    )
    {
        this.pillarSpawnSettings = pillarSpawnSettings;
        this.randomProvider = randomProvider;
    }
    
    public void StartTimedSpawning (Timer timer)
    {
        RemoveTimerListeners();
        currentTimer = timer;
        IPillarSettings currentDifficulty = pillarSpawnSettings.PillarDifficulty[currentDifficultyIndex];
        currentTimer.WaitTime = currentDifficulty.PillarSpawnInterval;
        currentTimer.Start();
        AddTimerListeners();
    }
    
    public Vector2 GetNewRandomSpawningPoint ()
    {
        float range = (float)randomProvider.Range(
            pillarSpawnSettings.PillarSpawnMinYHeight,
            pillarSpawnSettings.PillarSpawnMaxYHeight
        );
        Vector2 point = new(0, range);
        return point;
    }

    void HandleTimerTimeout ()
    {
        OnPillarSpawn?.Invoke();
    }
    
    void AddTimerListeners ()
    {
        currentTimer.Timeout += HandleTimerTimeout;
    }
    
    void RemoveTimerListeners ()
    {
        if (currentTimer != null)
            currentTimer.Timeout -= HandleTimerTimeout;
    }

    public void Dispose ()
    {
        RemoveTimerListeners();
    }
}