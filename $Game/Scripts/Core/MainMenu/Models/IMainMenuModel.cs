using System;

public interface IMainMenuModel : IDisposable
{
    IMainMenuWorld2DModel MainMenuWorld2DModel { get; }
    IMainMenuUICanvasModel MainMenuUICanvasModel { get; }
    
    void Initialize ();
}