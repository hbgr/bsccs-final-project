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

    private float currentSize;

    private float CurrentSize
    {
        get => currentSize;
        set => currentSize = value;
    }

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
        CurrentSize = spawnSize;
        transform.localScale = Vector3.one * CurrentSize;
        StartCoroutine(AbyssCoroutine(CurrentSize + growthRate));
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
            Shrink(0.05f);
        }
    }

    private void Shrink(float amount)
    {
        CurrentSize -= amount;
    }

    private IEnumerator AbyssCoroutine(float targetSize)
    {
        var scale = Vector3.one * Mathf.Min(targetSize, maxSize);
        var startScale = transform.localScale;

        var target = Mathf.Min(targetSize, maxSize);

        float t = 0;
        float rate = Mathf.Abs(CurrentSize - target * 0.9f) * Time.fixedDeltaTime / (cycleDuration * 0.25f);
        while (t <= cycleDuration * 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                // transform.localScale = Vector3.Lerp(startScale, scale * 0.9f, t / (cycleDuration * 0.25f));
                CurrentSize -= rate;
                transform.localScale = Vector3.one * CurrentSize;
            }

            yield return new WaitForFixedUpdate();
        }

        t = 0;
        rate = Mathf.Abs(CurrentSize - target * 1.1f) * Time.fixedDeltaTime / (cycleDuration * 0.5f);
        while (t <= cycleDuration * 0.5f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                // transform.localScale = Vector3.Lerp(scale * 0.9f, scale * 1.1f, t / (cycleDuration * 0.5f));
                CurrentSize += rate;
                transform.localScale = Vector3.one * CurrentSize;
            }

            yield return new WaitForFixedUpdate();
        }

        // if (targetSize >= maxSize)
        // {
        //     // Shoot abyssal orbs
        //     var directions = new List<Vector2> { new(-1, -1), new(-1, 1), new(1, -1), new(1, 1) };
        //     foreach (var dir in directions)
        //     {
        //         var orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
        //         orb.SetDirection(dir);
        //     }
        //     // events.OnPlayAudio(gameObject, orbAudio);
        // }

        t = 0;
        rate = Mathf.Abs(CurrentSize - target) * Time.fixedDeltaTime / (cycleDuration * 0.25f);
        while (t <= cycleDuration * 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                // transform.localScale = Vector3.Lerp(scale * 1.1f, scale, t / (cycleDuration * 0.25f));
                CurrentSize -= rate;
                transform.localScale = Vector3.one * CurrentSize;
            }

            yield return new WaitForFixedUpdate();
        }

        health++;

        StartCoroutine(AbyssCoroutine(CurrentSize + growthRate));
        yield return null;
    }
}
