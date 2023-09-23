using Godot;

public partial class PillarManagerNode : Node, IPillarManagerNode
{
    [Export]
    Vector3 PillarSpawnPosition { get; set; }
    
    [Export]
    Timer PillarSpawnTimer { get; set; }

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

    void AddModelListeners ()
    {
        
    }

    void RemoveModelListeners ()
    {
        
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}