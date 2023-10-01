using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviourExtended
{
    [SerializeField]
    private ScriptableInputEvents inputEvents;

    [SerializeField]
    private GameObject uiObject;

    // Start is called before the first frame update
    void Start()
    {
        inputEvents.OnAction2Event += OnAction2;
    }    

    protected override void OnDestroy()
    {
        base.OnDestroy();

        inputEvents.OnAction2Event -= OnAction2;
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

    private void OnAction2(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
