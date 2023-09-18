using System;
using Godot;
using static GlobalSettings.Paths.Loading;
using Array = Godot.Collections.Array;

public partial class LoadingScope : SingletonNode<LoadingScope>
{
    #region Nodes
    public ILoadingNode LoadingNode { get; private set; }
    #endregion

    bool isLoading;
    PackedScene sceneToLoad;
    Node nodeToUnload;
    
    public override void _Ready ()
    {
        InitializeScope();
        ProcessNodesInitialization();
        AddNodeListeners();
    }
    
    public override void _ExitTree ()
    {
        ProcessNodesDisposal();
    }

    public override void _Process (double delta)
    {
        LoadSceneOperation();
    }

    public void Load (PackedScene scene, Node unloadNode = null)
    {
        LoadingNode.FadeIn();
        sceneToLoad = scene;
        nodeToUnload = unloadNode;
    }
    
    void AddNodeListeners ()
    {
        LoadingNode.OnFadeInAnimationFinished += HandleFadeInAnimationFinished;
        LoadingNode.OnFadeOutAnimationFinished += HandleFadeOutAnimationFinished;
    }
    
    void LoadSceneOperation ()
    {
        if (!isLoading)
            return;

        Array loadingProgress = new();
        ResourceLoader.ThreadLoadStatus loadStatus =
            ResourceLoader.LoadThreadedGetStatus(sceneToLoad.ResourcePath, loadingProgress);
        
        switch (loadStatus)
        {
            case ResourceLoader.ThreadLoadStatus.InvalidResource:
                throw new InvalidOperationException(
                    "Could not load scene in the specified path!");
            case ResourceLoader.ThreadLoadStatus.InProgress:
                Variant progress = loadingProgress[0];
                LoadingNode.SetLoadingProgress(progress.AsDouble());
                break;
            case ResourceLoader.ThreadLoadStatus.Failed:
                throw new InvalidOperationException(
                    "The loading process has failed!");
            case ResourceLoader.ThreadLoadStatus.Loaded:
                PackedScene scene = (PackedScene)ResourceLoader.LoadThreadedGet(sceneToLoad.ResourcePath);
                Node sceneNode = scene.Instantiate();
                GetTree().Root.AddChild(sceneNode);
                LoadingNode.FadeOut();
                LoadingNode.SetLoadingProgress(100);
                isLoading = false;
                sceneToLoad = null;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void InitializeScope ()
    {
        base._Ready();
        PackedScene loadingNodeScene = GD.Load<PackedScene>(LOADING_NODE_SCENE_PATH);
        LoadingNode loadingNode = loadingNodeScene.Instantiate<LoadingNode>();
        AddChild(loadingNode);
        LoadingNode = loadingNode;
    }
    
    void ProcessNodesInitialization ()
    {
        LoadingNode.Initialize();
    }

    void ProcessNodesDisposal ()
    {
        LoadingNode.Dispose();
    }
    
    void HandleFadeInAnimationFinished ()
    {
        if (isLoading)
            return;
        isLoading = true;
        ResourceLoader.LoadThreadedRequest(sceneToLoad.ResourcePath);
        nodeToUnload?.QueueFree();
        nodeToUnload = null;
    }
    
    void HandleFadeOutAnimationFinished ()
    {
        
    }
}