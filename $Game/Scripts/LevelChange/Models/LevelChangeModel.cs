using System;
using System.Collections.Generic;

public class LevelChangeModel : ILevelChangeModel
{
    public event Action<int> OnLevelChanged;
    
    readonly ILevelChangeSettings settings;
    
    IPillarManagerModel pillarManagerModel;

    ILevelChangeUniqueSettings CurrentLevelChange => settings.LevelChanges[currentLevelChangeIndex];
    int currentLevelChangeIndex;

    public LevelChangeModel (
        ILevelChangeSettings settings, 
        IPillarManagerModel pillarManagerModel
    )
    {
        this.settings = settings;
        this.pillarManagerModel = pillarManagerModel;
    }
    
    public void Initialize ()
    {
        AddModelListeners();
    }
        
    void HandlePillarPassed ()
    {
        IReadOnlyList<ILevelChangeUniqueSettings> levelChanges = settings.LevelChanges;
        if (currentLevelChangeIndex >= levelChanges.Count - 1)
            return;

        int pillarsPassed = pillarManagerModel.PillarsPassedCount;
        ILevelChangeUniqueSettings nextLevelChange = levelChanges[currentLevelChangeIndex + 1];

        if (pillarsPassed < nextLevelChange.PillarsPassedRequirement) 
            return;
        
        currentLevelChangeIndex++;
        OnLevelChanged?.Invoke(CurrentLevelChange.Id);
    }
    
    void AddModelListeners ()
    {
        pillarManagerModel.OnPillarPassed += HandlePillarPassed;
    }
        
    void RemoveModelListeners ()
    {
        pillarManagerModel.OnPillarPassed -= HandlePillarPassed;
    }

    public void Dispose ()
    {
        RemoveModelListeners();
    }
}