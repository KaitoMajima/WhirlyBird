public class MapWorld2DModel : IMapWorld2DModel
{
    public IPlayerModel PlayerModel { get; }
    public IPillarManagerModel PillarManagerModel { get; }
    public ILevelChangeModel LevelChangeModel { get; }

    public MapWorld2DModel (
        IPlayerModel playerModel, 
        IPillarManagerModel pillarManagerModel,
        ILevelChangeModel levelChangeModel
    )
    {
        PlayerModel = playerModel;
        PillarManagerModel = pillarManagerModel;
        LevelChangeModel = levelChangeModel;
    }
    
    public void Initialize ()
    {
        PlayerModel.Initialize();
        PillarManagerModel.Initialize();
        LevelChangeModel.Initialize();
    }
    
    public void Dispose ()
    {
        PlayerModel.Dispose();
        PillarManagerModel.Dispose();
        LevelChangeModel.Dispose();
    }
}