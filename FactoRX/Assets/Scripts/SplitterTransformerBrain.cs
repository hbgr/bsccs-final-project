using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "TransformerBrain/Splitter")]
public class SplitterTransformerBrain : TransformerBrain
{
    [SerializeField]
    private List<Vector3> splitDirections;

    public override IEnumerator TransformerCoroutine(Transformer transformer, PowerOrbController powerOrb)
    {
        if (splitDirections.Count < 1)
        {
            yield break;
        }

        powerOrb.MultiplyLifetime(1f / splitDirections.Count);

        var powerOrbs = new List<PowerOrbController>();

        foreach (var direction in splitDirections)
        {
            PowerOrbController orb = Instantiate(powerOrb, transformer.transform.position + transformer.transform.rotation * direction * 0.55f, Quaternion.identity);
            orb.SetProperties(transformer.transform.rotation * direction, transformer.gameObject);
            orb.gameObject.SetActive(false);
            powerOrbs.Add(orb);
        }

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

        foreach (var orb in powerOrbs)
        {
            orb.gameObject.SetActive(true);
        }
        orbAudio.Play(transformer.gameObject);

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
