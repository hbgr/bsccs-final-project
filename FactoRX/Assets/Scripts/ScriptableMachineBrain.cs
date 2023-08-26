using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableMachineBrain : ScriptableObject
{
    public abstract IEnumerator MachineCoroutine(Machine machine, ScriptableArenaProperties arenaProps);
    protected bool Enabled => GameStateManager.IsState(GameState.Game);
}
