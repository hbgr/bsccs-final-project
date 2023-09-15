using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TransformerBrain/Redirector")]
public class RedirectorTransformerBrain : TransformerBrain
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float boostFactor;

    public override IEnumerator TransformerCoroutine(Transformer transformer, PowerOrbController powerOrb)
    {
        powerOrb.MultiplyLifetime(boostFactor);
        PowerOrbController orb = Instantiate(powerOrb, transformer.transform.position, Quaternion.identity);
        orb.SetProperties(transformer.transform.rotation * direction, transformer.gameObject);
        orb.gameObject.SetActive(false);
        Destroy(powerOrb.gameObject);

        var scale = transformer.Scale;

        float t = 0;
        while (t <= 0.05f)
        {
            if (transformer.Enabled)
            {
                transformer.transform.localScale = Vector3.Lerp(scale, scale * 1.2f, t / 0.05f);
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        orb.gameObject.SetActive(true);

        t = 0;
        while (t <= 0.05f)
        {
            if (transformer.Enabled)
            {
                transformer.transform.localScale = Vector3.Lerp(transformer.transform.localScale, scale, t / 0.05f);
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        transformer.transform.localScale = scale;

        yield return null;
    }
}
