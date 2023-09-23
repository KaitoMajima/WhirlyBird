using System;

public interface IPillarManagerNode : IDisposable
{
    void Setup (IPillarManagerModel model);
    void Initialize ();
}