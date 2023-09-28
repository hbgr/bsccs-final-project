using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/IncreaseLifetime")]
public class IncreaseLifetimeLevelUpBehaviour : LevelUpBehaviour
{
    [SerializeField]
    private PowerOrbManager powerOrbManager;

    [SerializeField]
    private float amount;

    public override void Apply()
    {
        powerOrbManager.OrbLifetime += amount;
    }
}
