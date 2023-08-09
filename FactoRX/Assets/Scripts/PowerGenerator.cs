using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    [SerializeField]
    private float cooldown;

    [SerializeField]
    private PowerOrbController powerOrbPrefab;

    [SerializeField]
    private float orbSpeed;

    private List<BasicMachineController> machineList;

    // Start is called before the first frame update
    void Start()
    {
        machineList = new List<BasicMachineController>();
        StartCoroutine(PowerCoroutine(cooldown));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator PowerCoroutine(float cooldown)
    {
        // Shoot power orb
        var powerOrb = Instantiate(powerOrbPrefab, transform.position, Quaternion.identity);
        powerOrb.SetProperties(orbSpeed, machineList);
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(PowerCoroutine(cooldown));
    }

    public void AddMachine(BasicMachineController machine)
    {
        machineList.Add(machine);
    }
}
