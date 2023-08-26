using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private ScriptableCollectable collectable;

    public void Collect()
    {
        collectable.Collect(this);
    }
}
