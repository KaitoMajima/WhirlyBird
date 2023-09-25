﻿using System;

public interface IMapWorld2DModel : IDisposable
{
    IPlayerModel PlayerModel { get; }
    IPillarManagerModel PillarManagerModel { get; }
    ILevelChangeModel LevelChangeModel { get; }
    
    void Initialize ();
}