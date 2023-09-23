using Godot;

public partial class PillarManagerNode : Node
{
    [Export]
    Vector2 PillarSpawnPosition { get; set; }
    
    [Export]
    Timer PillarSpawnTimer { get; set; }

    [Export] 
    Node PillarSpawnParent { get; set; }

    [Export] 
    PackedScene PillarNodePack;

    readonly DualStatePool<PillarNode> pillarPool = new();

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
        PillarSpawnParent.AddChild(pillar);
        pillar.Initialize();
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