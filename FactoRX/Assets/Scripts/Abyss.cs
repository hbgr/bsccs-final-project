using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Abyss : MonoBehaviourExtended
{
    [SerializeField]
    private float spawnSize;

    [SerializeField]
    private float maxSize;

    [SerializeField]
    private float cycleDuration;

    [SerializeField]
    private int health;

    [SerializeField]
    private AbyssalOrb orbPrefab;

    [SerializeField]
    private ScriptableAudio spawnAudio;

    [SerializeField]
    private ScriptableAudio shootAudio;

    [SerializeField]
    private ScriptableAudio damagedAudio;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * spawnSize;
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.TryGetComponent(out PowerOrbController powerOrb) && powerOrb.CanCollideWith(gameObject))
        {
            Destroy(collider.gameObject);
            TakeDamage(1);
        }
    }

    private void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            events.OnScorePoints(this, 500);
            events.OnGainPercentLevelEvent(this, 0.8f);
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        spawnAudio.Play(gameObject);
        float t = 0;
        while (t <= 0.5f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(Vector3.one * spawnSize, Vector3.one * maxSize, t / 0.5f);
            }
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(AbyssCoroutine(cycleDuration));
        yield return null;
    }

    private IEnumerator AbyssCoroutine(float cycleDuration)
    {
        float t = 0;
        while (t <= cycleDuration * 0.45f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }

        spawnAudio.Play(gameObject);

        t = 0;
        while (t <= 0.05)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(Vector3.one * maxSize, 1.1f * maxSize * Vector3.one, t / 0.05f);
            }
            yield return new WaitForFixedUpdate();
        }


        Vector3 dir = Vector3Int.RoundToInt((Vector3.zero - transform.position).normalized);
        dir = dir.normalized;
        List<Vector3> directions = new()
        {
            dir,
            Quaternion.AngleAxis(45, Vector3.forward) * dir,
            Quaternion.AngleAxis(-45, Vector3.forward) * dir
        };

        foreach (var direction in directions)
        {
            AbyssalOrb orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
            orb.SetDirection(direction);
        }

        t = 0;
        while (t <= 0.05)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(1.1f * maxSize * Vector3.one, Vector3.one * maxSize, t / 0.05f);
            }
            yield return new WaitForFixedUpdate();
        }

        t = 0;
        while (t <= cycleDuration * 0.45f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(AbyssCoroutine(cycleDuration));
        yield return null;
    }
}
