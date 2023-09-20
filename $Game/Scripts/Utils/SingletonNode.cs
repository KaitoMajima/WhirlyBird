using Godot;

public partial class SingletonNode<T> : Node where T : SingletonNode<T>
{
    public static T Instance { get; private set; }
    
    public override void _Ready ()
    {
        if (Instance != null)
            QueueFree();
        else
            Instance = (T)this;
    }
}