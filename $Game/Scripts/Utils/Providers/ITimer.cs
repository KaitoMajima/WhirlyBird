using System;

public interface ITimer
{
    event Action Timeout;
    
    double WaitTime { get; set; }
    bool OneShot { get; set; }
    bool Autostart { get; set;  }
    bool Paused { get; set; }
    double TimeLeft { get; }

    void Start (double timeSec = -1.0);
    void Stop ();
    bool IsStopped ();
}