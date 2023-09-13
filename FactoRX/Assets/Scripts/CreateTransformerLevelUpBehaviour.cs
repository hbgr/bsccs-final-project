using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpBehaviour/CreateTransformer")]
public class CreateTransformerLevelUpBehaviour : LevelUpBehaviour
{
    [SerializeField]
    private Transformer transformer;

    [SerializeField]
    private TransformVariable playerTransform;

    public override void Apply()
    {
        Transformer t = Instantiate(transformer, playerTransform.value.position, Quaternion.identity);
    }
}
