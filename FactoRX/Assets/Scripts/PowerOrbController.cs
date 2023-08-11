using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbController : MonoBehaviour
{
    private HashSet<BasicMachineController> targetMachines;

    private float moveSpeed;

    private BasicMachineController target;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(moveSpeed * Time.deltaTime * moveDirection);
        if (Vector3.Distance(transform.position, target.transform.position) < 0.25f)
        {
            // Activate machine
            target.ActivateMachine();

            // Remove machine from machine set
            targetMachines.Remove(target);

            // Find new closest machine
            if (targetMachines.Count <= 0)
            {
                Destroy(gameObject);
                return;
            }

            target = null;
            foreach (var machine in targetMachines)
            {
                if (target == null)
                {
                    target = machine;
                    continue;
                }

                if (Vector3.Distance(transform.position, machine.transform.position) < Vector3.Distance(transform.position, target.transform.position))
                {
                    target = machine;
                }
            }

            moveDirection = (target.transform.position - transform.position).normalized;
        }
    }

    public void SetProperties(float speed, List<BasicMachineController> machines)
    {
        targetMachines = new HashSet<BasicMachineController>(machines);

        if (machines.Count <= 0)
        {
            Destroy(gameObject);
            return;
        }

        moveSpeed = speed;

        target = null;
        foreach (var machine in targetMachines)
        {
            if (target == null)
            {
                target = machine;
                continue;
            }

            if (Vector3.Distance(transform.position, machine.transform.position) < Vector3.Distance(transform.position, target.transform.position))
            {
                target = machine;
            }
        }

        moveDirection = (target.transform.position - transform.position).normalized;
    }
}
