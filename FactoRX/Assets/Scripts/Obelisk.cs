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
    private float shootDelayGrowth;

    [SerializeField]
    private float minShootdelay;

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

    [SerializeField]
    private Color activeColour;

    [SerializeField]
    private Color inactiveColour;

    private bool activated;

    private SpriteRenderer render;


    // Start is called before the first frame update
    void Start()
    {
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
            events.OnGainPercentLevelEvent(this, 0.75f);
            events.OnScorePoints(this, 100);
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
        StartCoroutine(AbsorbCoroutine());
    }

    private void SetActive(bool active)
    {
        if (active)
        {
            energyText.gameObject.SetActive(true);
            activationAudio.Play(gameObject);
            render.sprite = activeSprite;
            render.color = activeColour;
            SetEnergy(maxEnergy);
            activated = true;
        }
        else
        {
            activated = false;
            energyText.gameObject.SetActive(false);
            render.sprite = inactiveSprite;
            render.color = inactiveColour;
        }
    }

    private IEnumerator ActivationCoroutine(float delay)
    {
        SetActive(false);

        float t = 0;
        while (t <= delay - 3.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        yield return StartCoroutine(FlashColour(inactiveColour, activeColour, 0.5f));

        t = 0;
        while (t <= 0.33f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        yield return StartCoroutine(FlashColour(inactiveColour, activeColour, 0.5f));

        t = 0;
        while (t <= 0.33f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        yield return StartCoroutine(FlashColour(inactiveColour, activeColour, 0.5f));

        t = 0;
        while (t <= 0.33f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        t = 0;
        while (t <= 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                render.color = Color.Lerp(inactiveColour, activeColour, t / 0.25f);
            }
            yield return new WaitForFixedUpdate();
        }

        SetActive(true);

        StartCoroutine(ShootingCoroutine());
        yield return null;
    }

    private IEnumerator ShootingCoroutine()
    {
        var scale = transform.localScale;
        int randRotation = UnityEngine.Random.Range(0, 45);
        int shotCount = 0;
        float delay = shootDelay;

        while (activated)
        {
            float t = 0;
            while (t <= 0.07f)
            {
                if (Enabled)
                {
                    t += Time.fixedDeltaTime;
                    transform.localScale = Vector3.Lerp(scale, 1.1f * scale, t / 0.07f);
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

            transform.localScale = scale;

            t = 0;
            while (t <= delay)
            {
                if (Enabled)
                {
                    t += Time.fixedDeltaTime;
                }
                yield return new WaitForFixedUpdate();
            }

            shotCount++;
            delay = Mathf.Max(minShootdelay, delay - shootDelayGrowth);

            yield return new WaitForFixedUpdate();
        }

        maxEnergy += maxEnergyGrowth;

        yield return null;
    }

    private IEnumerator AbsorbCoroutine()
    {
        float t = 0;
        while (t <= 0.1f)
        {
            if (!activated)
            {
                yield break;
            }

            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                render.color = Color.Lerp(activeColour, Color.white, t / 0.1f);
            }
            yield return new WaitForFixedUpdate();
        }

        t = 0;
        while (t <= 0.1f)
        {
            if (!activated)
            {
                yield break;
            }

            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                render.color = Color.Lerp(Color.white, activeColour, t / 0.1f);
            }
            yield return new WaitForFixedUpdate();
        }

        render.color = activeColour;

        yield return null;
    }

    private IEnumerator FlashColour(Color colourA, Color colourB, float duration)
    {
        float t = 0;
        while (t <= duration / 2f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                render.color = Color.Lerp(colourA, colourB, t / (duration / 2f));
            }
            yield return new WaitForFixedUpdate();
        }

        t = 0;
        while (t <= duration / 2f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                render.color = Color.Lerp(colourB, colourA, t / (duration / 2f));
            }
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}
