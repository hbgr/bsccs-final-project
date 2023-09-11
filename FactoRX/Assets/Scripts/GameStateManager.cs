using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public ScriptableGameState gameState;

    [SerializeField]
    private ScriptableGameEvents events;

    private static GameStateManager instance;

    public static GameStateManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        events.LevelUpEvent += OnLevelUp;
        events.LevelUpCompletedEvent += OnLevelUpCompleted;
        SetGameState(GameState.Game);
    }

    private void OnLevelUp(object sender, EventArgs args)
    {
        SetGameState(GameState.LevelUpMenu);
    }

    private void OnLevelUpCompleted(object sender, EventArgs args)
    {
        SetGameState(GameState.Game);
    }

    public static bool IsState(GameState state)
    {
        return instance.gameState.State == state;
    }

    public static bool IsState(List<GameState> states)
    {
        return states.Contains(instance.gameState.State);
    }

    private void SetGameState(GameState state)
    {
        // gameState.SetGameState(state);
        StartCoroutine(SetGameStateCoroutine(state));
    }

    private IEnumerator SetGameStateCoroutine(GameState state)
    {
        yield return new WaitForFixedUpdate();
        gameState.SetGameState(state);
    }
}
