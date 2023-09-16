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


    // Start is called before the first frame update
    void Start()
    {

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

        t = 0;
        while (t <= 0.5f * cooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }


        yield return null;
    }
}
