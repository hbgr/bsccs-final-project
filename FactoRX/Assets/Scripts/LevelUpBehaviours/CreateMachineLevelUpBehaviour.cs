using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/CreateMachine")]
public class CreateMachineLevelUpBehaviour : LevelUpBehaviour
{
    [SerializeField]
    private Machine machinePrefab;

    [SerializeField]
    private TransformVariable playerTransform;

    public override void Apply()
    {
        Machine m = Instantiate(machinePrefab, Vector3Int.RoundToInt(playerTransform.value.position), Quaternion.identity);
    }
}
