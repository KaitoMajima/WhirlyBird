using System.Collections.Generic;
using Godot;

public partial class PillarManagerNode : Node
{
    [Export] Vector2 pillarSpawnPosition;
    [Export] TestableTimer pillarSpawnTimer;
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
        model.SetTimer(pillarSpawnTimer);
        model.StartSpawning();
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
        pillarModel.Setup(model.PillarSecondsUntilDestruction);
        pillarModel.SetTimer(pillar.PillarDestructionTimer);
        pillarModel.Initialize();
        
        Vector2 newSpawnPosition = new(
            pillarSpawnPosition.X, 
            pillarSpawnPosition.Y + model.GetNewRandomSpawningPoint()
        );
        pillar.SetPosition(newSpawnPosition);
        pillar.SetActive(true);
        pillar.SetSpeed(model.PillarSpeed);
        pillar.Setup(pillarModel);
        pillar.Initialize();
        pillarPool.InsertAsActive(pillar);
        
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
        
        model.StartSpawning();
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