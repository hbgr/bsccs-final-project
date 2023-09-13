using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : MonoBehaviourExtended
{
    [SerializeField]
    private float growthRate;

    [SerializeField]
    private float size;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.one * size;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        size += growthRate * Time.deltaTime;
        transform.localScale = Vector2.one * size;
    }

    private void Shrink()
    {
        size -= 0.1f;
        transform.localScale = Vector2.one * size;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.TryGetComponent(out PowerOrbController powerOrb) && powerOrb.CanCollideWith(gameObject))
        {
            Destroy(collider.gameObject);
            Shrink();
        }
    }
}
