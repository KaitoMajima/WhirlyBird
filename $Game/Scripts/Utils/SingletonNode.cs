using Godot;
using System.Collections.Generic;

public partial class SingletonNode : Node
{
    static readonly Dictionary<string, Node> instances = new();

    protected T RegisterSingletonInstance<T>(T instance) where T : Node
    {
        string typeName = typeof(T).FullName;

        if (instances.ContainsKey(typeName!))
            instance.QueueFree();
        else
            instances[typeName] = instance;

        T registeredInstance = (T)instances[typeName];
        return registeredInstance;
    }
}