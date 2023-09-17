using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCollectable : ScriptableObject
{
    [SerializeField]
    protected ScriptableAudio collectAudio;

    public abstract void Collect(Collectable collectable);
}
