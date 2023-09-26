﻿public class ParallaxManagerModel : IParallaxManagerModel
{
    public float ParallaxOffset { get; private set; }
        
    readonly IPillarManagerModel pillarManagerModel;

    public ParallaxManagerModel (IPillarManagerModel pillarManagerModel)
    {
        this.pillarManagerModel = pillarManagerModel;
    }

    public void Initialize ()
    {
        HandleDifficultyChanged();
        AddLevelChangeListeners();
    }
    
    void HandleDifficultyChanged ()
    {
        ParallaxOffset = pillarManagerModel.ParallaxBaseValue * pillarManagerModel.ParallaxMultiplier;
    }

    void AddLevelChangeListeners ()
    {
        pillarManagerModel.OnPillarDifficultyChanged += HandleDifficultyChanged;
    }
    
    void RemoveLevelChangeListeners ()
    {
        pillarManagerModel.OnPillarDifficultyChanged -= HandleDifficultyChanged;
    }

    public void Dispose ()
    {
        RemoveLevelChangeListeners();
    }
}