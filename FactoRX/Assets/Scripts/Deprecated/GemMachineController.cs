using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMachineController : MonoBehaviour, IMachine
{
    public Transform Transform => transform;

    public void Activate()
    {
        StartCoroutine(MachineProcessCoroutine());
    }

    public bool CanBePickedUp()
    {
        return !isActive;
    }

    public void SetArenaController(ArenaController arenaController)
    {
        this.arenaController = arenaController;
    }

    [SerializeField]
    private GemController gemPrefab;

    private ArenaController arenaController;

    private bool isActive = false;

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

    private IEnumerator MachineProcessCoroutine()
    {
        // isActive = true;

        // var renderer = GetComponent<SpriteRenderer>();
        // var colour = new Color(renderer.color.r, renderer.color.g, renderer.color.b);
        // var scale = transform.localScale;

        // float t = 0;
        // while (t <= 0.5f)
        // {
        //     renderer.color = Color.Lerp(colour, Color.cyan, t / 0.5f);
        //     transform.localScale = Vector3.Lerp(scale, scale * 1.2f, t / 0.5f);
        //     t += Time.deltaTime;
        //     yield return new WaitForEndOfFrame();
        // }

        // for (int i = 0; i < 3; i++)
        // {
        //     Vector2 spawnPos = 0.8f * arenaController.GetCurrentRadius() * Random.insideUnitCircle;
        //     GemController gem = Instantiate(gemPrefab, spawnPos, Quaternion.identity);
        //     yield return new WaitForSeconds(0.75f);
        // }

        // yield return new WaitForSeconds(0.5f);

        // t = 0;
        // while (t <= 0.75f)
        // {
        //     renderer.color = Color.Lerp(renderer.color, colour, t / 0.5f);
        //     transform.localScale = Vector3.Lerp(transform.localScale, scale, t / 0.5f);
        //     t += Time.deltaTime;
        //     yield return new WaitForEndOfFrame();
        // }

        // renderer.color = colour;
        // transform.localScale = scale;

        // isActive = false;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out PowerOrbController powerOrb))
        {
            if (!isActive)
            {
                Destroy(collider.gameObject);
                Activate();
            }
            else
            {
                powerOrb.transform.position = transform.position;
                powerOrb.SetProperties(Vector2.down);
            }
        }
    }    
}
