using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
public class Rotatable : MonoBehaviour
{
    public void RotateBy(float amount)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, amount));
    }    
}
