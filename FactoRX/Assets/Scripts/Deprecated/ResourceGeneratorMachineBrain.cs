using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MachineBrain/ResourceGeneratorMachineBrain")]
public class ResourceGeneratorMachineBrain : ScriptableMachineBrain
{
    [SerializeField]
    private GameObject resourcePrefab;

    public override IEnumerator MachineCoroutine(Machine machine, ScriptableArenaProperties arenaProps)
    {
        // // Shoot power orb
        // var powerOrb = Instantiate(powerOrbPrefab, transform.position, Quaternion.identity);
        // // powerOrb.SetProperties(orbSpeed, machineList);
        // powerOrb.SetProperties(orbSpeed, Vector2.up, gameObject);
        // yield return new WaitForSeconds(cooldown);
        // StartCoroutine(PowerCoroutine(cooldown));
        yield return null;
    }
}
