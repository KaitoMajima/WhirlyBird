﻿using Godot;

public partial class PillarManagerNode : Node, IPillarManagerNode
{
    [Export]
    Vector2 PillarSpawnPosition { get; set; }
    
    [Export]
    Timer PillarSpawnTimer { get; set; }

    [Export] 
    Node PillarSpawnParent { get; set; }

    [Export] 
    PackedScene PillarNodePack;

    readonly DualStatePool<IPillarNode> pillarPool = new();

    IPillarManagerModel model;
    
    public void Setup (IPillarManagerModel model)
    {
        this.model = model;
    }

    public void Initialize ()
    {
        AddModelListeners();
        model.StartTimedSpawning(PillarSpawnTimer);
    }
    
    void HandlePillarSpawn ()
    {
        PillarNode pillar = PillarNodePack.Instantiate<PillarNode>();
        pillar.SetPosition(PillarSpawnPosition);
        pillar.OnPillarScoreTriggered += HandlePillarScoreTriggered;
        pillar.OnPillarDamageTriggered += HandlePillarDamageTriggered;
        PillarSpawnParent.AddChild(pillar);
        pillar.Initialize();
    }

    void HandlePillarScoreTriggered ()
    {
        GD.Print("Scored!");
    }
    
    void HandlePillarDamageTriggered ()
    {
        GD.Print("Damaged!");
    }
    
    void AddModelListeners ()
    {
        model.OnPillarSpawn += HandlePillarSpawn;
    }
    
    void RemoveModelListeners ()
    {
        model.OnPillarSpawn -= HandlePillarSpawn;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}