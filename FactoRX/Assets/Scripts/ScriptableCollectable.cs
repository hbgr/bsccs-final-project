using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCollectable : ScriptableObject
{
    [SerializeField]
    protected ScriptableAudio collectAudio;

    [SerializeField]
    protected float lifetime;

    public float Lifetime => lifetime;

    public abstract void Collect(Collectable collectable);
}
