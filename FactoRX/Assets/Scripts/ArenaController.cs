using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [SerializeField]
    private float startRadius;

    [SerializeField]
    private float maxRadius;

    [SerializeField]
    private float currentRadius;

    [SerializeField]
    private float shrinkRate;

    [SerializeField]
    private float shrinkRateIncreaseInterval;

    [SerializeField]
    private float gemSpawnDelay;

    [SerializeField]
    private float gemSpawnStartDelay;

    [SerializeField]
    private GemController gemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentRadius = startRadius;
        SetRadius(startRadius);
        StartCoroutine(OnStartCoroutine(gemSpawnStartDelay, gemSpawnDelay, shrinkRateIncreaseInterval));
    }

    // Update is called once per frame
    void Update()
    {
        SetRadius(currentRadius - shrinkRate * Time.deltaTime);
    }

    private void SetRadius(float radius)
    {
        currentRadius = Mathf.Min(radius, maxRadius);
        transform.localScale = new Vector3(2 * radius, 2 * radius, 1);
    }

    public void IncreaseRadius(float amount)
    {
        StartCoroutine(IncreaseRadiusCoroutine(amount));
    }

    public float GetCurrentRadius()
    {
        return currentRadius;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Lost game!");
        }
    }

    private IEnumerator OnStartCoroutine(float startDelay, float spawnDelay, float rateIncDelay)
    {
        StartCoroutine(ShrinkRateIncreaseCoroutine(rateIncDelay));
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(GemSpawnCoroutine(spawnDelay));
        yield return null;
    }

    private IEnumerator GemSpawnCoroutine(float spawnDelay)
    {
        Vector2 randPos = 0.8f * currentRadius * Random.insideUnitCircle;
        var gem = Instantiate(gemPrefab, transform.position + (Vector3)randPos, Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(GemSpawnCoroutine(spawnDelay));
        yield return null;
    }

    private IEnumerator ShrinkRateIncreaseCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        shrinkRate *= 1.33f;
        StartCoroutine(ShrinkRateIncreaseCoroutine(delay));
        yield return null;
    }

    private IEnumerator IncreaseRadiusCoroutine(float amount)
    {
        float t = 0;
        while (t <= 0.25f)
        {
            SetRadius(currentRadius + Time.deltaTime / 0.25f * amount);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
