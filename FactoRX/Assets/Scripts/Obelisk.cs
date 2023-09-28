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
    private int maxEnergyGrowth;

    [SerializeField]
    private int energyLossOnHit;

    [SerializeField]
    private TextMeshPro energyText;

    [SerializeField]
    private AbyssalOrb orbPrefab;

    [SerializeField]
    private float shootDelay;

    [SerializeField]
    private float firstActivationDelay;

    [SerializeField]
    private float deactivationDuration;

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
        activated = false;
        render = GetComponent<SpriteRenderer>();
        StartCoroutine(ActivationCoroutine(firstActivationDelay));
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        if (!activated) return;

        if (energy <= 0)
        {
            StartCoroutine(ActivationCoroutine(deactivationDuration));
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
        energy = math.min(maxEnergy, math.max(e, 0));
        energyText.text = $"{energy:F0}";
    }

    private void AbsorbPower()
    {
        float e = energy - energyLossOnHit;
        e = math.max(e, 0);
        SetEnergy(e);
    }

    private IEnumerator ActivationCoroutine(float delay)
    {
        activated = false;
        energyText.gameObject.SetActive(false);
        render.sprite = inactiveSprite;

        float t = 0;
        while (t <= delay)
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
        SetEnergy(maxEnergy);
        activated = true;

        StartCoroutine(ShootingCoroutine());
        yield return null;
    }

    private IEnumerator ShootingCoroutine()
    {
        var scale = transform.localScale;
        int randRotation = UnityEngine.Random.Range(0, 45);
        int shotCount = 0;

        while (activated)
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
                Quaternion.AngleAxis(randRotation + shotCount * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(45 + randRotation + shotCount * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(90 + randRotation + shotCount * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(135 + randRotation + shotCount * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(180 + randRotation + shotCount * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(225 + randRotation + shotCount * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(270 + randRotation + shotCount * 15, Vector3.forward) * dir,
                Quaternion.AngleAxis(315 + randRotation + shotCount * 15, Vector3.forward) * dir,
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
            while (t <= shootDelay)
            {
                if (Enabled)
                {
                    t += Time.fixedDeltaTime;
                }
                yield return new WaitForFixedUpdate();
            }

            shotCount++;

            yield return new WaitForFixedUpdate();
        }

        maxEnergy += maxEnergyGrowth;

        yield return null;
    }
}
