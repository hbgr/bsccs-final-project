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

    private bool acceptInput;

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

            // Generate weighted list of indexes for level up behaviours
            var weighted_index_list = new List<int>();
            for (int i = 0; i < levelUpBehaviourStore.LevelUpBehaviours.Count; i++)
            {
                weighted_index_list.AddRange(Enumerable.Repeat(i, levelUpBehaviourStore.LevelUpBehaviours[i].Weight));
            }

            // Select from weighted list without repetition
            var selected_indexes = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                int index = UnityEngine.Random.Range(0, weighted_index_list.Count);
                selected_indexes.Add(weighted_index_list[index]);
                weighted_index_list.RemoveAll(x => x == weighted_index_list[index]);
            }

            option1.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[selected_indexes[0]]);
            option2.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[selected_indexes[1]]);
            option3.SetProperties(levelUpBehaviourStore.LevelUpBehaviours[selected_indexes[2]]);

            StartCoroutine(WhenEnabledCoroutine());
        }
        else
        {
            menuObject.SetActive(false);
            acceptInput = false;
        }
    }

    private void SelectLevelUpOption(LevelUpMenuOption option)
    {
        option.Select();
        events.OnLevelUpCompleted(this, EventArgs.Empty);
    }

    private void OnAction1(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled || !acceptInput) return;

        if (context.performed)
        {
            SelectLevelUpOption(option1);
        }
    }

    private void OnAction2(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled || !acceptInput) return;

        if (context.performed)
        {
            SelectLevelUpOption(option2);
        }
    }

    private void OnAction3(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled || !acceptInput) return;

        if (context.performed)
        {
            SelectLevelUpOption(option3);
        }
    }

    private IEnumerator WhenEnabledCoroutine()
    {
        float t = 0f;
        while (t <= 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        acceptInput = true;
        yield return null;
    }
}
