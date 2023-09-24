using System;

public interface IGameOverModel : IDisposable
{
    event Action OnGameOverTriggered;
    void Intialiize ();
}