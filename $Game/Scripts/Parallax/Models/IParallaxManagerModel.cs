using System;

public interface IParallaxManagerModel : IDisposable
{
    float ParallaxOffset { get; }
    
    void Initialize ();
}