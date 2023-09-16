using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MachineBrain/SplitterMachineBrain")]
public class SplitterMachineBrain : ScriptableMachineBrain
{
    [SerializeField]
    private float orbSpeed;

    [SerializeField]
    private PowerOrbController powerOrbPrefab;

    public override IEnumerator MachineCoroutine(Machine machine, ScriptableArenaProperties arenaProps)
    {
        // Instantiate(powerOrbPrefab, machine.transform.position + machine.transform.rotation * Vector3.up * 0.5f, Quaternion.identity).SetProperties(orbSpeed, machine.transform.rotation * Vector3.up, machine.gameObject);
        // Instantiate(powerOrbPrefab, machine.transform.position, Quaternion.identity).SetProperties(orbSpeed, machine.transform.rotation * Vector3.down, machine.gameObject);
        yield return null;
    }
}
