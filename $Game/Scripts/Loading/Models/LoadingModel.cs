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
    
    PackedScene sceneToLoad;
    Node nodeToUnload;
    
    public void SetupLoad (PackedScene scene, Node unloadNode = null)
    {
        sceneToLoad = scene;
        nodeToUnload = unloadNode;
        OnLoadingStarted?.Invoke();
    }
    
    public void InitiateLoading ()
    {
        IsLoading = true;
        
        ResourceLoader.LoadThreadedRequest(sceneToLoad.ResourcePath);
        nodeToUnload?.QueueFree();
        nodeToUnload = null;
        OnLoadingInitiated?.Invoke();
    }

    public void ProcessLoading ()
    {
        Array loadingProgress = new();
        ResourceLoader.ThreadLoadStatus loadStatus =
            ResourceLoader.LoadThreadedGetStatus(sceneToLoad.ResourcePath, loadingProgress);
        
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
        PackedScene scene = (PackedScene)ResourceLoader.LoadThreadedGet(sceneToLoad.ResourcePath);
        sceneToLoad = null;
        IsLoading = false;
        OnLoadingFinished?.Invoke(scene);
    }
}