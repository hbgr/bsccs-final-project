using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCollectable : ScriptableObject
{
    public abstract void Collect(Collectable collectable);
}
