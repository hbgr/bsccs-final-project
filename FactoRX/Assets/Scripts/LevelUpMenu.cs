using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelUpMenu : MonoBehaviourExtended
{
    [SerializeField]
    private ScriptableInputEvents inputEvents;

    [SerializeField]
    private GameObject menuObject;

    [SerializeField]
    private LevelUpMenuOption option1;

    [SerializeField]
    private LevelUpMenuOption option2;

    [SerializeField]
    private LevelUpMenuOption option3;

    [SerializeField]
    private List<LevelUpBehaviour> levelUpBehaviours;

    private LevelUpBehaviour levelUpBehaviour1;

    private LevelUpBehaviour levelUpBehaviour2;

    private LevelUpBehaviour levelUpBehaviour3;

    // Start is called before the first frame update
    void Start()
    {
        events.GameStateChangedEvent += OnGameStateChanged;
        inputEvents.OnAction1Event += OnAction1;
        inputEvents.OnAction2Event += OnAction2;
        inputEvents.OnAction3Event += OnAction3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnGameStateChanged(object sender, GameState state)
    {
        if (activeGameStates.Contains(state))
        {
            menuObject.SetActive(true);

            levelUpBehaviour1 = levelUpBehaviours[0];
            levelUpBehaviour2 = levelUpBehaviours[1];
            levelUpBehaviour3 = levelUpBehaviours[2];

            option1.SetProperties(levelUpBehaviour1);
            option2.SetProperties(levelUpBehaviour2);
            option3.SetProperties(levelUpBehaviour3);
        }
        else
        {
            menuObject.SetActive(false);
        }
    }

    private void SelectLevelUpOption(LevelUpBehaviour levelUpBehaviour)
    {
        levelUpBehaviour.Apply();
        events.OnLevelUpCompleted(this, EventArgs.Empty);
    }

    private void OnAction1(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SelectLevelUpOption(levelUpBehaviour1);
        }
    }

    private void OnAction2(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SelectLevelUpOption(levelUpBehaviour2);
        }
    }

    private void OnAction3(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SelectLevelUpOption(levelUpBehaviour3);
        }
    }
}
