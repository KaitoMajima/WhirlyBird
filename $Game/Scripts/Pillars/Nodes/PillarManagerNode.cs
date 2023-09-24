using System.Collections.Generic;
using Godot;

public partial class PillarManagerNode : Node
{
    [Export] Vector2 pillarSpawnPosition;
    [Export] Timer pillarSpawnTimer;
    [Export] Node pillarSpawnParent;
    [Export] PackedScene pillarNodePack;

    readonly DualStatePool<PillarNode> pillarPool = new();

    IPillarManagerModel model;
    
    public void Setup (IPillarManagerModel model)
    {
        this.model = model;
    }

    public void Initialize ()
    {
        AddModelListeners();
        model.StartTimedSpawning(pillarSpawnTimer);
    }
    
    PillarNode CreatePillar ()
    {
        PillarNode pillar = pillarNodePack.Instantiate<PillarNode>();
        pillar.SetPosition(pillarSpawnPosition);
        pillarSpawnParent.AddChild(pillar);
        return pillar;
    }
    
    void SetupPillar (PillarNode pillar)
    {
        IPillarModel pillarModel = PillarFactory.CreatePillarModel();
        pillarModel.SetSecondsUntilDestruction(model.PillarSecondsUntilDestruction);
        pillarPool.InsertAsActive(pillar);
        pillar.SetPosition(pillarSpawnPosition + model.GetNewRandomSpawningPoint());
        pillar.SetActive(true);
        pillar.SetSpeed(model.PillarSpeed);
        
        pillar.Setup(pillarModel);
        pillar.Initialize();
        
        AddPillarNodeListeners(pillar);
    }
    
    void HandlePillarSpawn ()
    {
        PillarNode pillar = pillarPool.Fetch() ?? CreatePillar();
        SetupPillar(pillar);
    }
    
    void HandlePillarDifficultyChanged ()
    {
        foreach (PillarNode pillar in pillarPool.ActiveItemsSet)
            pillar.SetSpeed(model.PillarSpeed);
        
        model.StartTimedSpawning(pillarSpawnTimer);
    }

    void HandlePillarMarkedForDestruction (PillarNode pillar)
    {
        pillar.SetActive(false);
        pillarPool.InsertAsInactive(pillar);
        RemovePillarNodeListeners(pillar);
    }
    
    void AddModelListeners ()
    {
        model.OnPillarSpawn += HandlePillarSpawn;
        model.OnPillarDifficultyChanged += HandlePillarDifficultyChanged;
    }

    void AddPillarNodeListeners (PillarNode pillar)
    {
        pillar.OnPillarMarkedForDestruction += HandlePillarMarkedForDestruction;
    }
    
    void RemoveModelListeners ()
    {
        model.OnPillarSpawn -= HandlePillarSpawn;
        model.OnPillarDifficultyChanged -= HandlePillarDifficultyChanged;
    }
    
    void RemovePillarNodeListeners (PillarNode pillar)
    {
        pillar.OnPillarMarkedForDestruction -= HandlePillarMarkedForDestruction;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        
        HashSet<PillarNode> activePillars = pillarPool.ActiveItemsSet;

        foreach (PillarNode activePillar in activePillars)
        {
            RemovePillarNodeListeners(activePillar);
            activePillar.Dispose();
        }
        
        base.Dispose();
    }
}