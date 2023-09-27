﻿using System;
using System.Collections.Generic;

public class LevelChangeModel : ILevelChangeModel
{
    public event Action<int> OnLevelChanged;
    
    readonly ILevelChangeSettings settings;
    readonly IPillarManagerModel pillarManagerModel;
    readonly IMusicManagerModel musicManagerModel;

    ILevelChangeUniqueSettings CurrentLevelChange => settings.LevelChanges[currentLevelChangeIndex];
    int currentLevelChangeIndex;

    public LevelChangeModel (
        ILevelChangeSettings settings, 
        IPillarManagerModel pillarManagerModel,
        IMusicManagerModel musicManagerModel
    )
    {
        this.settings = settings;
        this.pillarManagerModel = pillarManagerModel;
        this.musicManagerModel = musicManagerModel;
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
        
        //TODO: Refactor this to an external class
        if (CurrentLevelChange.Id == 1)
            musicManagerModel.Play(MusicClipType.Level1);
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