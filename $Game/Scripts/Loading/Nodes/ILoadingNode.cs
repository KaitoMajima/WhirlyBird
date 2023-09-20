using System;

public interface ILoadingNode : IDisposable
{
    void Setup (ILoadingModel model);
    
    void Initialize ();
}