using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/ShieldPlayer")]
public class ApplyShieldLevelUpBehaviour : LevelUpBehaviour
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private float duration;

    public override void Apply()
    {
        events.OnShieldCollected(this, duration);
    }
}
