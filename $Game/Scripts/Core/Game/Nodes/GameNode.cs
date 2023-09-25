using Godot;

public partial class GameNode : Node
{
    [Export]
    public ConfigResource ConfigResource { get; private set; }
    
    [Export]
    public MusicResource MusicResource { get; private set; }
    
    [Export]
    public MusicManagerNode MusicManagerNode { get; private set; }

    public void Setup (IMusicManagerModel musicManagerModel)
    {
        MusicManagerNode.Setup(musicManagerModel);
    }
    
    public void Initialize ()
    {
        MusicManagerNode.Initialize();
    }

    public new void Dispose ()
    {
        MusicManagerNode.Dispose();
    }
}