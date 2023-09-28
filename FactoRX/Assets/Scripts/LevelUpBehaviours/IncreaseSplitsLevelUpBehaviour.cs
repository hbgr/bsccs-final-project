using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/AddMaxSplits")]
public class IncreaseSplitsLevelUpBehaviour : LevelUpBehaviour
{
    [SerializeField]
    private PowerOrbManager powerOrbManager;

    [SerializeField]
    private int amount;

    public override void Apply()
    {
        powerOrbManager.MaxSplits += amount;
    }
}
