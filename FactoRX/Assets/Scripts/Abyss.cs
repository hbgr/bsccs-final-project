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
    private float minSize;

    private float currentSize;

    [SerializeField]
    private float cycleDuration;

    [SerializeField]
    private float growthRate;

    [SerializeField]
    private float shrinkAmount;

    [SerializeField]
    private ScriptableAudio pulseAudio;

    [SerializeField]
    private DamagesPlayer damageZone;

    // Start is called before the first frame update
    void Start()
    {
        currentSize = spawnSize;
        transform.localScale = Vector3.one * currentSize;
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
            Shrink(shrinkAmount);
        }
    }

    private void Shrink(float amount)
    {
        currentSize -= amount;
        if (currentSize < minSize)
        {
            events.OnScorePoints(this, 1000);
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        damageZone.gameObject.SetActive(false);
        float t = 0;
        while (t <= 0.5f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }

        damageZone.gameObject.SetActive(true);

        StartCoroutine(AbyssCoroutine(currentSize + growthRate));
        yield return null;
    }

    private IEnumerator AbyssCoroutine(float targetSize)
    {
        var target = Mathf.Min(targetSize, maxSize);

        float t = 0;
        float rate = Mathf.Abs(currentSize - target * 0.9f) * Time.fixedDeltaTime / (cycleDuration * 0.25f);
        while (t <= cycleDuration * 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                // transform.localScale = Vector3.Lerp(startScale, scale * 0.9f, t / (cycleDuration * 0.25f));
                currentSize -= rate;
                transform.localScale = Vector3.one * currentSize;
            }

            yield return new WaitForFixedUpdate();
        }

        pulseAudio.Play(gameObject);
        t = 0;
        rate = Mathf.Abs(currentSize - target * 1.1f) * Time.fixedDeltaTime / (cycleDuration * 0.5f);
        while (t <= cycleDuration * 0.5f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                currentSize += rate;
                transform.localScale = Vector3.one * currentSize;
            }

            yield return new WaitForFixedUpdate();
        }

        t = 0;
        rate = Mathf.Abs(currentSize - target) * Time.fixedDeltaTime / (cycleDuration * 0.25f);
        while (t <= cycleDuration * 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                currentSize -= rate;
                transform.localScale = Vector3.one * currentSize;
            }

            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(AbyssCoroutine(currentSize + growthRate));
        yield return null;
    }
}
