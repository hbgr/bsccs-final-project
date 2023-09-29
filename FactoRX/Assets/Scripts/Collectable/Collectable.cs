using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviourExtended
{
    [SerializeField]
    private ScriptableCollectable collectable;

    private float duration;

    private void Start()
    {
        duration = collectable.Lifetime;
    }

    private void Update()
    {
        if (!Enabled) return;

        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        collectable.Collect(this);
    }
}
