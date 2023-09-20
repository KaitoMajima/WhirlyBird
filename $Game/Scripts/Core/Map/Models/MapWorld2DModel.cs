public class MapWorld2DModel : IMapWorld2DModel
{
    public IPlayerModel PlayerModel { get; }
    
    public MapWorld2DModel (IPlayerModel playerModel)
    {
        PlayerModel = playerModel;
    }
    
    public void Initialize ()
    {
        
    }
    
    public void Dispose ()
    {
        
    }
}