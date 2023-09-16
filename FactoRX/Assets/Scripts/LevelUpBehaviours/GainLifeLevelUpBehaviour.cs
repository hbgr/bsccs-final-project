using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/GainLives")]
public class GainLifeLevelUpBehaviour : LevelUpBehaviour
{
    [SerializeField]
    private LivesManager lives;

    [SerializeField]
    private int amount;

    public override void Apply()
    {
        lives.Lives += amount;
    }
}
