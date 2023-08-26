using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GemStore")]
public class GemStore : ScriptableObject
{
    [SerializeField]
    private ScriptableGameEvents events;

    private int gems;

    public int Gems
    {
        get => gems;
        private set
        {
            gems = value;
            OnGemValueChanged(this, this);
        }
    }

    private void OnEnable()
    {
        gems = 0;
        events.GemCollectedEvent += OnGemCollected;
    }

    private void OnGemCollected(object sender, int value)
    {
        Gems += value;
        // check if level up
        // if level up trigger level up event, which will enable reward ui etc
    }

    public event EventHandler<GemStore> GemValueChangedEvent;

    private void OnGemValueChanged(object sender, GemStore gemStore)
    {
        GemValueChangedEvent?.Invoke(sender, gemStore);
    }
}
