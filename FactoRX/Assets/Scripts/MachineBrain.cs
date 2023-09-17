using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MachineBrain : ScriptableObject
{
    [SerializeField]
    protected int activationEnergy;

    [SerializeField]
    protected ScriptableAudio activationAudio;

    public int ActivationEnergy => activationEnergy;

    public abstract IEnumerator MachineCoroutine(Machine machine);
}
