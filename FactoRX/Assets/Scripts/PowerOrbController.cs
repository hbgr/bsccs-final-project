using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbController : MonoBehaviourExtended
{
    // private HashSet<IMachine> targetMachines;

    private float moveSpeed = 2f;

    // private IMachine target;

    private Vector2 moveDirection;

    private HashSet<GameObject> hitList;

    [SerializeField]
    private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        if (hitList == null)
        {
            hitList = new HashSet<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        transform.position += (Vector3)(moveSpeed * Time.deltaTime * moveDirection);
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    // public void SetProperties(float speed, List<IMachine> machines)
    // {
    //     targetMachines = new HashSet<IMachine>(machines);

    //     if (machines.Count <= 0)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }

    //     moveSpeed = speed;

    //     target = null;
    //     foreach (var machine in targetMachines)
    //     {
    //         if (target == null)
    //         {
    //             target = machine;
    //             continue;
    //         }

    //         if (Vector3.Distance(transform.position, machine.Transform.position) < Vector3.Distance(transform.position, target.Transform.position))
    //         {
    //             target = machine;
    //         }
    //     }

    //     moveDirection = (target.Transform.position - transform.position).normalized;
    // }

    public void SetProperties(float speed, Vector2 direction, GameObject creator)
    {
        moveSpeed = speed;
        moveDirection = direction;
        if (hitList == null)
        {
            hitList = new HashSet<GameObject>();
        }
        hitList.Add(creator);
    }

    public void SetProperties(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void SetProperties(Vector2 direction, GameObject creator)
    {
        moveDirection = direction;
        if (hitList == null)
        {
            hitList = new HashSet<GameObject>();
        }
        hitList.Add(creator);
    }

    public bool CanCollideWith(GameObject go)
    {
        if (!hitList.Contains(go))
        {
            return true;
        }
        return false;
    }

    public void MultiplyLifetime(float amount)
    {
        lifetime *= amount;
    }
}
