public class MapWorld2DModel : IMapWorld2DModel
{
    public IPlayerModel PlayerModel { get; }
    public IPillarManagerModel PillarManagerModel { get; }
    public ILevelChangeModel LevelChangeModel { get; }
    public IParallaxManagerModel ParallaxManagerModel { get; }

    public MapWorld2DModel (
        IPlayerModel playerModel, 
        IPillarManagerModel pillarManagerModel,
        ILevelChangeModel levelChangeModel,
        IParallaxManagerModel parallaxManagerModel
    )
    {
        PlayerModel = playerModel;
        PillarManagerModel = pillarManagerModel;
        LevelChangeModel = levelChangeModel;
        ParallaxManagerModel = parallaxManagerModel;
    }
    
    public void Initialize ()
    {
        PlayerModel.Initialize();
        PillarManagerModel.Initialize();
        LevelChangeModel.Initialize();
        ParallaxManagerModel.Initialize();
    }
    
    public void Dispose ()
    {
        PlayerModel.Dispose();
        PillarManagerModel.Dispose();
        LevelChangeModel.Dispose();
        ParallaxManagerModel.Dispose();
    }
}