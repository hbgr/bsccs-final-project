using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Abyss : MonoBehaviourExtended
{
    [SerializeField]
    private float spawnSize;

    [SerializeField]
    private float maxSize;

    [SerializeField]
    private float cycleDuration;

    [SerializeField]
    private float growthRate;

    [SerializeField]
    private int health;

    [SerializeField]
    private AbyssalOrb orbPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AbyssCoroutine(spawnSize));
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.TryGetComponent(out PowerOrbController powerOrb) && powerOrb.CanCollideWith(gameObject))
        {
            Destroy(collider.gameObject);
            health--;
        }
    }

    private IEnumerator AbyssCoroutine(float targetSize)
    {
        var scale = Vector3.one * Mathf.Min(targetSize, maxSize);
        var startScale = transform.localScale;

        float t = 0;
        while (t <= cycleDuration * 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(startScale, scale * 0.9f, t / (cycleDuration * 0.25f));
            }

            yield return new WaitForFixedUpdate();
        }

        t = 0;
        while (t <= cycleDuration * 0.5f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(scale * 0.9f, scale * 1.1f, t / (cycleDuration * 0.5f));
            }

            yield return new WaitForFixedUpdate();
        }

        if (targetSize >= maxSize)
        {
            // Shoot abyssal orbs
            var directions = new List<Vector2> { new(-1, -1), new(-1, 1), new(1, -1), new(1, 1) };
            foreach (var dir in directions)
            {
                var orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
                orb.SetDirection(dir);
            }
            // events.OnPlayAudio(gameObject, orbAudio);
        }

        t = 0;
        while (t <= cycleDuration * 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(scale * 1.1f, scale, t / (cycleDuration * 0.25f));
            }

            yield return new WaitForFixedUpdate();
        }

        health++;

        StartCoroutine(AbyssCoroutine(targetSize + growthRate));
        yield return null;
    }
}
