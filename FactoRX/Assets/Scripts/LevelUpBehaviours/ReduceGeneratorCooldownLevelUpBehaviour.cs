using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/ReduceGeneratorCooldown")]
public class ReduceGeneratorCooldownLevelUpBehaviour : LevelUpBehaviour
{
    [SerializeField]
    private PowerOrbManager powerOrbManager;

    [SerializeField]
    private float amount;

    public override void Apply()
    {
        powerOrbManager.GeneratorCooldown -= amount;
    }
}
