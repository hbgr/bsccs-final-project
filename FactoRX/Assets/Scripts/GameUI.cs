using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviourExtended
{
    [SerializeField]
    private GameObject uiObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
