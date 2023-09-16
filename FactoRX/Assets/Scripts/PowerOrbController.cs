using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbController : MonoBehaviourExtended
{
    [SerializeField]
    private float moveSpeed = 2f;

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
