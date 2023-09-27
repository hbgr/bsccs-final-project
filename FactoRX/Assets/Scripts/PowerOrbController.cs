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

    [SerializeField]
    private float maxLifetime;

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

    private void SetLifetime(float newLifetime)
    {
        lifetime = Mathf.Min(newLifetime, maxLifetime);
    }

    public void MultiplyLifetime(float amount)
    {
        SetLifetime(lifetime * amount);
    }

    public void AddLifetime(float amount)
    {
        SetLifetime(lifetime + amount);
    }
}
