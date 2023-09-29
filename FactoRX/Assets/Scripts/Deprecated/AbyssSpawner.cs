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
    private ScriptableArenaProperties arenaProps;

    private HashSet<Abyss> activeAbysses;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAbyssCoroutine(spawnDelay, 0));
        activeAbysses = new();
        events.AbyssDestroyedEvent += OnAbyssDestroyed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        events.AbyssDestroyedEvent -= OnAbyssDestroyed;
    }

    private IEnumerator SpawnAbyssCoroutine(float cooldown, int cycleCount)
    {
        float t = 0;
        while (t <= 0.33f * cooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        // spawn abyss
        // var spawnPos = Vector3Int.RoundToInt(Random.insideUnitCircle.normalized * (arenaProps.radius - 0.5f));
        var spawnPos = ((Vector3)Vector3Int.RoundToInt(Random.insideUnitCircle.normalized)).normalized * (arenaProps.radius - 0.5f);
        Abyss abyss = Instantiate(abyssPrefab, spawnPos, Quaternion.identity);
        activeAbysses.Add(abyss);

        t = 0;
        while (t <= 0.66f * cooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(SpawnAbyssCoroutine(cooldown, cycleCount + 1));

        yield return null;
    }

    private void OnAbyssDestroyed(object sender, Abyss e)
    {
        activeAbysses.Remove(e);
    }
}
