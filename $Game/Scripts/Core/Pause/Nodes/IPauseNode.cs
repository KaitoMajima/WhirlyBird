using System;

public interface IPauseNode : IDisposable
{
    PauseMenuCenterButtons CenterButtons { get; }

    void Setup (IPauseModel pauseModel);
    void Initialize ();
}