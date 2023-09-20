using System;
using Godot;

public interface ILoadingModel
{
    event Action OnLoadingStarted;
    event Action OnLoadingInitiated;
    event Action<double> OnLoadingProgress;
    event Action<PackedScene> OnLoadingFinished;
    
    bool IsLoading { get; }

    void SetupLoad (PackedScene scene, Node unloadNode = null);
    void InitiateLoading ();
    void ProcessLoading ();
}