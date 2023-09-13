using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourExtended : MonoBehaviour
{
    [SerializeField]
    protected ScriptableGameEvents events;

    [SerializeField]
    protected List<GameState> activeGameStates;

    public virtual bool Enabled => GameStateManager.IsState(activeGameStates) && enabled;

    protected virtual void OnGameStateChanged(object sender, GameState state)
    {

    }

    protected virtual void Awake()
    {
        events.GameStateChangedEvent += OnGameStateChanged;
    }

    protected virtual void OnDestroy()
    {
        events.GameStateChangedEvent -= OnGameStateChanged;
    }
}
