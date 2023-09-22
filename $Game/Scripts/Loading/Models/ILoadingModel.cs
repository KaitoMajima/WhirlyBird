using System;
using Godot;

public interface ILoadingModel : IDisposable
{
    event Action OnLoadingStarted;
    event Action OnLoadingInitiated;
    event Action<double> OnLoadingProgress;
    event Action<PackedScene> OnLoadingFinished;
    
    bool IsLoading { get; }

    void Setup (LoadingConfigResource loadingConfig);
    void StartLoad (string scenePath, Node unloadNode = null);

    void InitiateLoading ();
    void ProcessLoading ();
}