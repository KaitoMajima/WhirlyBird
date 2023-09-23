using System;

public interface IMapInputDetectionNode : IDisposable
{
    void Setup (IMapInputDetectionModel inputDetectionModel);

    void Initialize ();
}