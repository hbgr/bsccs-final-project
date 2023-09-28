using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerOrbController : MonoBehaviourExtended
{
    [SerializeField]
    private PowerOrbManager powerOrbManager;    

    private Vector2 moveDirection;

    private HashSet<GameObject> hitList;

    [SerializeField]
    private float lifetime;

    // [SerializeField]
    // private float maxLifetime;

    [SerializeField]
    private int splitCount;

    public bool CanSplit => splitCount < powerOrbManager.MaxSplits;

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

        transform.position += (Vector3)(powerOrbManager.OrbSpeed * Time.deltaTime * moveDirection);
        lifetime += Time.deltaTime;

        if (lifetime >= powerOrbManager.OrbLifetime)
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

    // private void SetLifetime(float newLifetime)
    // {
    //     lifetime = Mathf.Min(newLifetime, maxLifetime);
    // }

    // public void MultiplyLifetime(float amount)
    // {
    //     SetLifetime(lifetime * amount);
    // }

    // public void AddLifetime(float amount)
    // {
    //     SetLifetime(lifetime + amount);
    // }

    public void Split(int count)
    {
        splitCount = math.max(0, splitCount + count);
    }
}
