public class MapWorld2DModel : IMapWorld2DModel
{
    public IPlayerModel PlayerModel { get; }
    public IPillarManagerModel PillarManagerModel { get; }

    public MapWorld2DModel (
        IPlayerModel playerModel, 
        IPillarManagerModel pillarManagerModel
    )
    {
        PlayerModel = playerModel;
        PillarManagerModel = pillarManagerModel;
    }
    
    public void Initialize ()
    {
        
    }
    
    public void Dispose ()
    {
        
    }
}