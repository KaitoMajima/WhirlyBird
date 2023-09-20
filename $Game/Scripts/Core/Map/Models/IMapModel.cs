﻿using System;

public interface IMapModel : IDisposable
{
    IMapUICanvasModel MapUICanvasModel { get; }
    IMapWorld2DModel MapWorld2DModel { get; }
    
    void Initialize ();
}