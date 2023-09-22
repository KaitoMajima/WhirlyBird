using System;
using Godot;
using Array = Godot.Collections.Array;

public class LoadingModel : ILoadingModel
{
    public event Action OnLoadingStarted;
    public event Action OnLoadingInitiated;
    public event Action<double> OnLoadingProgress;
    public event Action<PackedScene> OnLoadingFinished;

    public bool IsLoading { get; private set; }

    LoadingConfigResource loadingConfig;
    
    string sceneLoadPath;
    Node nodeToUnload;

    public void Setup (LoadingConfigResource loadingConfig)
    {
        this.loadingConfig = loadingConfig;
    }

    public void StartLoad (string scenePath, Node unloadNode = null)
    {
        sceneLoadPath = scenePath;
        nodeToUnload = unloadNode;
        OnLoadingStarted?.Invoke();
    }

    public void InitiateLoading ()
    {
        if (IsLoading)
        {
            GD.PushWarning("Warning: Tried to load a scene when a scene is already being loaded!");
            return;
        }
        
        IsLoading = true;
        
        Error error = ResourceLoader.LoadThreadedRequest(
            sceneLoadPath, useSubThreads: 
            loadingConfig.UseMultiThreadedLoading
        );
        
        if (error != Error.Ok)
            throw new InvalidOperationException("Could not load the scene in the specified path!");
        
        OnLoadingInitiated?.Invoke();
    }

    public void ProcessLoading ()
    {
        Array loadingProgress = new();
        ResourceLoader.ThreadLoadStatus loadStatus =
            ResourceLoader.LoadThreadedGetStatus(sceneLoadPath, loadingProgress);
        
        switch (loadStatus)
        {
            case ResourceLoader.ThreadLoadStatus.InvalidResource:
                HandleThreadStatusIsInvalidResource();
                break;
            case ResourceLoader.ThreadLoadStatus.InProgress:
                HandleThreadStatusIsInProgress(loadingProgress);
                break;
            case ResourceLoader.ThreadLoadStatus.Failed:
                HandleThreadStatusIsFailed();
                break;
            case ResourceLoader.ThreadLoadStatus.Loaded:
                HandleThreadLoadStatusIsLoaded();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void HandleThreadStatusIsInvalidResource ()
    {
        throw new InvalidOperationException(
            "Could not load scene in the specified path!"
        );
    }
    
    void HandleThreadStatusIsInProgress (Array loadingProgress)
    {
        Variant progress = loadingProgress[0];
        OnLoadingProgress?.Invoke(progress.AsDouble());
    }
    
    void HandleThreadStatusIsFailed ()
    {
        throw new InvalidOperationException(
            "The loading process has failed!"
        );
    }
    
    void HandleThreadLoadStatusIsLoaded ()
    {
        PackedScene scene = (PackedScene)ResourceLoader.LoadThreadedGet(sceneLoadPath);
        sceneLoadPath = null;
        IsLoading = false;
        nodeToUnload?.QueueFree();
        nodeToUnload = null;
        OnLoadingFinished?.Invoke(scene);
    }

    public void Dispose ()
    {
        
    }
}