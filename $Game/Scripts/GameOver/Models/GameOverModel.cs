﻿using System;

public class GameOverModel : IGameOverModel
{
    public event Action OnGameOverTriggered;
    
    readonly IPlayerModel playerModel;
    
    bool isGameOver;

    public GameOverModel (IPlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }

    public void Intialiize ()
    {
        AddPlayerListeners();
    }
    
    void HandlePlayerDamaged ()
    { 
        if (isGameOver)
            return;
        
        playerModel.Kill();
        isGameOver = true;
        OnGameOverTriggered?.Invoke();
    }
    
    void AddPlayerListeners ()
    {
        playerModel.OnPlayerDamaged += HandlePlayerDamaged;
    }

    void RemovePlayerListeners ()
    {
        playerModel.OnPlayerDamaged -= HandlePlayerDamaged;
    }

    public void Dispose ()
    {
        RemovePlayerListeners();
    }
}