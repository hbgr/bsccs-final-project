using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMachineController : MonoBehaviour, IMachine
{
    [SerializeField]
    private BulletController bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float arenaIncreaseRate;

    private ArenaController arenaController;

    public Transform Transform => transform;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnStartCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Work around for event being null during start
    private IEnumerator OnStartCoroutine()
    {
        yield return new WaitForEndOfFrame();
        Events.OnMachineCreated(this, this);
        yield return null;
    }

    private IEnumerator MachineProcessCoroutine(float increaseRate)
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
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(2f);

        t = 0;
        while (t <= 0.75f)
        {
            renderer.color = Color.Lerp(renderer.color, colour, t / 0.5f);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, t / 0.5f);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        renderer.color = colour;
        transform.localScale = scale;
        yield return null;
    }

    public void SetArenaController(ArenaController arenaController)
    {
        this.arenaController = arenaController;
    }

    public void Activate()
    {
        StartCoroutine(MachineProcessCoroutine(arenaIncreaseRate));
    }
}