using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourExtended : MonoBehaviour
{
    [SerializeField]
    protected ScriptableGameEvents events;

    [SerializeField]
    protected List<GameState> activeGameStates;

    public bool Enabled => GameStateManager.IsState(activeGameStates);

    protected virtual void OnGameStateChanged(object sender, GameState state)
    {

    }

    protected virtual void Awake()
    {
        events.GameStateChangedEvent += OnGameStateChanged;
    }
}
