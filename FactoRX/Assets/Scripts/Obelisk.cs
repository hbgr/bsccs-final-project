using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Obelisk : MonoBehaviourExtended
{
    private float energy;

    [SerializeField]
    private int maxEnergy;

    [SerializeField]
    private float energyInterval;

    private float elapsedInterval;

    [SerializeField]
    private float energyLossAmount;

    [SerializeField]
    private float energyLossGrowth;

    [SerializeField]
    private float energyLossGrowthInterval;

    [SerializeField]
    private int energyOnHit;

    [SerializeField]
    private TextMeshPro energyText;

    [SerializeField]
    private AbyssalOrb orbPrefab;

    private int shootCount;

    private int shootRepeats;

    [SerializeField]
    private float activationDelay;

    [SerializeField]
    private ScriptableAudio activationAudio;

    [SerializeField]
    private ScriptableAudio shootAudio;

    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private Sprite inactiveSprite;

    private bool activated;

    private SpriteRenderer render;


    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
        activated = false;
        energyText.text = $"{energy}";
        energyText.gameObject.SetActive(false);
        shootCount = 0;
        shootRepeats = 1;
        render = GetComponent<SpriteRenderer>();
        render.sprite = inactiveSprite;
        StartCoroutine(ActivateCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        if (!activated) return;

        elapsedInterval += Time.deltaTime;
        if (elapsedInterval >= energyInterval)
        {
            float overspill = elapsedInterval % energyInterval;
            elapsedInterval = overspill;
            SetEnergy(energy - energyLossAmount);
        }

        if (energy <= 0)
        {
            SetEnergy(maxEnergy);
            StartCoroutine(ShootCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.TryGetComponent(out PowerOrbController powerOrb) && powerOrb.CanCollideWith(gameObject))
        {
            Destroy(collider.gameObject);
            if (activated)
            {
                AbsorbPower();
            }
        }
    }

    private void SetEnergy(float e)
    {
        energy = math.min(100, math.max(e, 0));
        energyText.text = $"{energy:F}";
    }

    private void AbsorbPower()
    {
        float e = energy + energyOnHit;
        e = math.min(e, maxEnergy);
        SetEnergy(e);
    }

    private IEnumerator ActivateCoroutine()
    {
        float t = 0;
        while (t <= activationDelay)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }

        energyText.gameObject.SetActive(true);
        activationAudio.Play(gameObject);
        render.sprite = activeSprite;
        activated = true;
        StartCoroutine(EnergyLossGrowthCoroutine());
        yield return null;
    }

    private IEnumerator ShootCoroutine()
    {
        var scale = transform.localScale;
        int randRotation = UnityEngine.Random.Range(0, 45);

        for (int i = 0; i < shootRepeats; i++)
        {
            float t = 0;
            while (t <= 0.03f)
            {
                if (Enabled)
                {
                    t += Time.fixedDeltaTime;
                    transform.localScale = Vector3.Lerp(scale, 1.1f * scale, t / 0.03f);
                }
                yield return new WaitForFixedUpdate();
            }

            Vector3 dir = Vector3Int.RoundToInt((Vector3.zero - transform.position).normalized);
            dir = dir.normalized;

            List<Vector3> directions = new()
            {
                Quaternion.AngleAxis(randRotation + i * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(45 + randRotation + i * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(90 + randRotation + i * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(135 + randRotation + i * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(180 + randRotation + i * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(225 + randRotation + i * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(270 + randRotation + i * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(315 + randRotation + i * 15, Vector3.forward) * dir,
            };

            foreach (var direction in directions)
            {
                AbyssalOrb orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
                orb.SetDirection(direction);
            }

            t = 0;
            while (t <= 0.07f)
            {
                if (Enabled)
                {
                    t += Time.fixedDeltaTime;
                    transform.localScale = Vector3.Lerp(1.1f * scale, scale, t / 0.07f);
                }
                yield return new WaitForFixedUpdate();
            }

            t = 0;
            while (t <= 0.15f)
            {
                if (Enabled)
                {
                    t += Time.fixedDeltaTime;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        shootCount++;
        if (shootCount % 2 == 0)
        {
            shootRepeats++;
        }

        yield return null;
    }

    private IEnumerator EnergyLossGrowthCoroutine()
    {
        float t = 0;
        while (t <= energyLossGrowthInterval)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }

        energyLossAmount += energyLossGrowth;
        StartCoroutine(EnergyLossGrowthCoroutine());
        yield return null;
    }
}
