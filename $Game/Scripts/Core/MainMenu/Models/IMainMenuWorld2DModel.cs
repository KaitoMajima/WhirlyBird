using System;

public interface IMainMenuWorld2DModel : IDisposable
{
    IParallaxManagerModel ParallaxManagerModel { get; }
    
    void Initialize ();
}