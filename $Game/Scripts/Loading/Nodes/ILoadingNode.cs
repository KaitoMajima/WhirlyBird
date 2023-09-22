using System;

public interface ILoadingNode : IDisposable
{
    LoadingConfigResource LoadingConfigResource { get; }
    void Setup (ILoadingModel model);
    
    void Initialize ();
}