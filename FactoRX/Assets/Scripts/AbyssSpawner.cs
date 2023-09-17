using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssSpawner : MonoBehaviourExtended
{
    [SerializeField]
    private Abyss abyssPrefab;

    [SerializeField]
    private float spawnDelay;

    [SerializeField]
    private float safeZoneRadius;

    [SerializeField]
    private ScriptableArenaProperties arenaProps;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAbyssCoroutine(spawnDelay));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnAbyssCoroutine(float cooldown)
    {
        float t = 0;
        while (t <= 0.5f * cooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        // spawn abyss
        var spawnPos = Vector3Int.RoundToInt(Random.insideUnitCircle.normalized * Random.Range(safeZoneRadius, arenaProps.currentRadius));
        Abyss abyss = Instantiate(abyssPrefab, spawnPos, Quaternion.identity);

        t = 0;
        while (t <= 0.5f * cooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(SpawnAbyssCoroutine(cooldown));

        yield return null;
    }
}