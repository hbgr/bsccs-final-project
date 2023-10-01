using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviourExtended
{
    [SerializeField]
    private GameObject uiObject;    

    protected override void OnGameStateChanged(object sender, GameState state)
    {
        if (activeGameStates.Contains(state))
        {
            uiObject.SetActive(true);
        }
        else
        {
            uiObject.SetActive(false);
        }
    }
}
