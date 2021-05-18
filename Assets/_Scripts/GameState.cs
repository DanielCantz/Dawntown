using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : Singleton<GameState>
{
    private GameStateEnum _gameState;

    public delegate void StateChange();

    public StateChange OnStateChange;

    public GameStateEnum State
    {
        get => _gameState;
        set
        {
            _gameState = value;
            if (OnStateChange != null)
            {
                OnStateChange.Invoke();
            }
        }
    }
}

public enum GameStateEnum
{
    Alive,
    Dead
}
