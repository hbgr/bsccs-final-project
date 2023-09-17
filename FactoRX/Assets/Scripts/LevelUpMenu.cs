using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private LevelUpBehaviourStore levelUpBehaviourStore;

    // Start is called before the first frame update
    void Start()
    {
        inputEvents.OnAction1Event += OnAction1;
        inputEvents.OnAction2Event += OnAction2;
        inputEvents.OnAction3Event += OnAction3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        inputEvents.OnAction1Event -= OnAction1;
        inputEvents.OnAction2Event -= OnAction2;
        inputEvents.OnAction3Event -= OnAction3;
    }

    protected override void OnGameStateChanged(object sender, GameState state)
    {
        if (activeGameStates.Contains(state))
        {
            menuObject.SetActive(true);

            // Select from level up behaviourd without repetition
            var index_list = Enumerable.Range(0, levelUpBehaviourStore.LevelUpBehaviours.Count).ToList();
            var selected_indexes = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                int index = UnityEngine.Random.Range(0, index_list.Count);
                selected_indexes.Add(index_list[index]);
                index_list.RemoveAt(index);
            }

            option1.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[selected_indexes[0]]);
            option2.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[selected_indexes[1]]);
            option3.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[selected_indexes[2]]);

            // option1.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[UnityEngine.Random.Range(0, levelUpBehaviourStore.LevelUpBehaviours.Count)]);
            // option2.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[UnityEngine.Random.Range(0, levelUpBehaviourStore.LevelUpBehaviours.Count)]);
            // option3.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[UnityEngine.Random.Range(0, levelUpBehaviourStore.LevelUpBehaviours.Count)]);
        }
        else
        {
            menuObject.SetActive(false);
        }
    }

    private void SelectLevelUpOption(LevelUpMenuOption option)
    {
        option.Select();
        events.OnLevelUpCompleted(this, EventArgs.Empty);
    }

    private void OnAction1(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SelectLevelUpOption(option1);
        }
    }

    private void OnAction2(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SelectLevelUpOption(option2);
        }
    }

    private void OnAction3(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SelectLevelUpOption(option3);
        }
    }
}
