using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ArenaProperties")]
public class ScriptableArenaProperties : ScriptableObject
{
    public float startRadius;

    public float maxRadius;

    public float currentRadius;

    public float shrinkRate;

    public float shrinkRateIncreaseInterval;
}
