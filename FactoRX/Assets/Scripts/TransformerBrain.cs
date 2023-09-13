using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransformerBrain : ScriptableObject
{
    public abstract IEnumerator TransformerCoroutine(Transformer transformer, PowerOrbController powerOrb);
}
