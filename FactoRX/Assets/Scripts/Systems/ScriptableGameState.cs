using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Game,
    Menu,
    LevelUpMenu,
    GameOver
}

[CreateAssetMenu(menuName = "Systems/GameState")]
public class ScriptableGameState : ScriptableObject
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private GameState state;

    public GameState State
    {
        get => state;
        private set
        {
            state = value;
            events.OnGameStateChanged(this, value);
        }
    }

    public void SetGameState(GameState state)
    {
        if (State != state)
        {
            State = state;
        }
    }

    private void OnDisable()
    {
        State = GameState.None;
    }
}
