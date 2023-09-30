using System;

public interface ILevelChangeModel : IDisposable
{
    event Action OnLevelChanged;
    int CurrentLevelId { get; }

    void Initialize ();
}