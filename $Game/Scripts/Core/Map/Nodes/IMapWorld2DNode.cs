using System;

public interface IMapWorld2DNode : IDisposable
{
    PlayerController PlayerController { get; }
    IPillarManagerNode PillarManagerNode { get; }
    
    void Setup (
        IPlayerModel playerModel,
        IMapInputDetectionModel mapInputDetectionModel,
        IPillarManagerModel pillarManagerModel
    );
    void Initialize ();
}