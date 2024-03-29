using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviourExtended, IPickUpBehaviour
{
    [SerializeField]
    private PowerOrbManager powerOrbManager;

    [SerializeField]
    private PowerOrbController powerOrbPrefab;

    [SerializeField]
    private ScriptableAudio orbAudio;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PowerCoroutine());
    }    

    private IEnumerator PowerCoroutine()
    {
        float t = 0;
        while (t <= 0.7f * powerOrbManager.GeneratorCooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new
            WaitForFixedUpdate();
        }

        var scale = transform.localScale;

        t = 0;
        while (t <= 0.05f * powerOrbManager.GeneratorCooldown)
        {
            if (Enabled)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, scale * 1.2f, t / (0.05f * powerOrbManager.GeneratorCooldown));
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        var powerOrb = Instantiate(powerOrbPrefab, transform.position + transform.rotation * (Vector2.right / 2f), Quaternion.identity);
        powerOrb.SetProperties(transform.rotation * Vector2.right, gameObject);
        orbAudio.Play(gameObject);

        t = 0;
        while (t <= 0.05 * powerOrbManager.GeneratorCooldown)
        {
            if (Enabled)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, scale, t / (0.05f * powerOrbManager.GeneratorCooldown));
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
        transform.localScale = scale;

        t = 0;
        while (t <= 0.2f * powerOrbManager.GeneratorCooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(PowerCoroutine());
    }

    public bool CanBePickedUp()
    {
        return true;
    }

    public void OnPickUp()
    {
        enabled = false;
    }

    public void OnDrop()
    {
        enabled = true;
    }
}
