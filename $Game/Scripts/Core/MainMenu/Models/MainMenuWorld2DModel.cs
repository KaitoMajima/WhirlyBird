public class MainMenuWorld2DModel : IMainMenuWorld2DModel
{
    public IParallaxManagerModel ParallaxManagerModel { get; }

    public MainMenuWorld2DModel (IParallaxManagerModel parallaxManagerModel)
    {
        ParallaxManagerModel = parallaxManagerModel;
    }

    public void Initialize ()
    {
        ParallaxManagerModel.Initialize();
    }

    public void Dispose ()
    {
        ParallaxManagerModel.Dispose();
    }
}