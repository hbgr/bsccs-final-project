using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationIndicator : MonoBehaviourExtended
{
    public void SetPosition(Rotatable rotatable)
    {
        transform.position = rotatable.transform.position + Vector3.up * 0.75f;
    }
}
