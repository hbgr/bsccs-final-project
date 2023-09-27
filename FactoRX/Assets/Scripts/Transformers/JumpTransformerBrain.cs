using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TransformerBrain/Jumper")]
public class JumpTransformerBrain : TransformerBrain
{
    [SerializeField]
    private Vector3 direction;    

    public override IEnumerator TransformerCoroutine(Transformer transformer, PowerOrbController powerOrb)
    {
        PowerOrbController orb = Instantiate(powerOrb, transformer.transform.position + transformer.transform.rotation * direction * 1.55f, Quaternion.identity);
        orb.SetProperties(transformer.transform.rotation * direction, transformer.gameObject);
        orb.gameObject.SetActive(false);
        Destroy(powerOrb.gameObject);

        var scale = transformer.Scale;

        float t = 0;
        while (t <= 0.1f)
        {
            if (transformer.Enabled)
            {
                transformer.transform.localScale = Vector3.Lerp(scale, scale * 1.15f, t / 0.05f);
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        orb.gameObject.SetActive(true);
        orbAudio.Play(transformer.gameObject);

        t = 0;
        while (t <= 0.1f)
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
