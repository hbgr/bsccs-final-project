using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMachineController : MonoBehaviour
{
    [SerializeField]
    private BulletController bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float arenaIncreaseRate;

    [SerializeField]
    private float arenaIncreaseDuration;

    [SerializeField]
    private float cooldownDuration;

    [SerializeField]
    private ArenaController arenaController;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(OnStartCoroutine(cooldownDuration));
    }

    // Update is called once per frame
    void Update()
    {

    }

    // private IEnumerator OnStartCoroutine(float waitDuration)
    // {
    //     yield return new WaitForSeconds(waitDuration);
    //     StartCoroutine(MachineProcessCoroutine(arenaIncreaseDuration, cooldownDuration, arenaIncreaseRate));
    //     yield return null;
    // }

    private IEnumerator MachineProcessCoroutine(float increaseDuration, float waitDuration, float increaseRate)
    {
        var renderer = GetComponent<SpriteRenderer>();
        var colour = new Color(renderer.color.r, renderer.color.g, renderer.color.b);
        var scale = transform.localScale;

        float t = 0;
        while (t <= 0.5f)
        {
            renderer.color = Color.Lerp(colour, Color.red, t / 0.5f);
            transform.localScale = Vector3.Lerp(scale, scale * 1.2f, t / 0.5f);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < 3; i++)
        {
            Vector2 spawnPos = Random.insideUnitCircle.normalized * arenaController.GetCurrentRadius();
            BulletController bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            bullet.SetProperties(bulletSpeed, transform.position);
            arenaController.IncreaseRadius(increaseRate);
            yield return new WaitForSeconds(0.33f);
        }

        yield return new WaitForSeconds(2f);

        t = 0;
        while (t <= 0.5f)
        {
            renderer.color = Color.Lerp(renderer.color, colour, t / 0.5f);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, t / 0.5f);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        renderer.color = colour;
        transform.localScale = scale;

        // // Apply cooldown
        // yield return new WaitForSeconds(waitDuration);

        // // Restart coroutine
        // StartCoroutine(MachineProcessCoroutine(increaseDuration, waitDuration, increaseRate));
        yield return null;
    }

    public void SetArenaController(ArenaController arenaController)
    {
        this.arenaController = arenaController;
    }

    public void ActivateMachine()
    {
        StartCoroutine(MachineProcessCoroutine(arenaIncreaseDuration, cooldownDuration, arenaIncreaseRate));
    }
}
