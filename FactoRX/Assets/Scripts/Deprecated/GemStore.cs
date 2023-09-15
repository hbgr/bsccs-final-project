using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GemStore")]
public class GemStore : ScriptableObject
{
    // [SerializeField]
    // private ScriptableGameEvents events;

    // [SerializeField]
    // private int levelUpThresholdGrowth;

    // private int levelUpThreshold;

    // public int LevelUpThreshold
    // {
    //     get => levelUpThreshold;
    //     private set
    //     {
    //         levelUpThreshold = value;
    //         OnGemValueChanged(this, this);
    //     }
    // }

    // private int gems;

    // public int Gems
    // {
    //     get => gems;
    //     private set
    //     {
    //         gems = value;
    //         OnGemValueChanged(this, this);
    //     }
    // }

    // private void OnEnable()
    // {
    //     gems = 0;
    //     levelUpThreshold = levelUpThresholdGrowth;
    //     events.GemCollectedEvent += OnGemCollected;
    // }

    // private void OnDisable()
    // {
    //     events.GemCollectedEvent -= OnGemCollected;
    // }

    // private void OnGemCollected(object sender, int value)
    // {
    //     Gems += value;
    //     // check if level up
    //     if (Gems >= LevelUpThreshold)
    //     {
    //         events.OnLevelUp(this, EventArgs.Empty);
    //         int leftoverGems = Gems % LevelUpThreshold;
    //         Gems = leftoverGems;
    //         LevelUpThreshold += levelUpThresholdGrowth;
    //     }
    // }

    // public event EventHandler<GemStore> GemValueChangedEvent;

    // private void OnGemValueChanged(object sender, GemStore gemStore)
    // {
    //     GemValueChangedEvent?.Invoke(sender, gemStore);
    // }
}
