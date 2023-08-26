using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
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

    public ScriptableGameState gameState;

    public static bool IsState(GameState state)
    {
        return instance.gameState.state == state;
    }
}
