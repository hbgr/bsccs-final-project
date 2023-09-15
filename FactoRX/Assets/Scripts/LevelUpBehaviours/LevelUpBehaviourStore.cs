using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/LevelUpBehaviourStore")]
public class LevelUpBehaviourStore : ScriptableObject
{
    [SerializeField]
    private List<LevelUpBehaviour> levelUpBehaviours;

    public List<LevelUpBehaviour> LevelUpBehaviours => levelUpBehaviours;
}
