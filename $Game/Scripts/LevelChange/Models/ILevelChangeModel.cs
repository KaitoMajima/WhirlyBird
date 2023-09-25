using System;

public interface ILevelChangeModel : IDisposable
{
    event Action<int> OnLevelChanged;

    void Initialize ();
}