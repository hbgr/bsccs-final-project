using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        foreach (var direction in splitDirections)
        {
            Instantiate(powerOrb, transformer.transform.position, Quaternion.identity).SetProperties(transformer.transform.rotation * direction, transformer.gameObject);
        }

        Destroy(powerOrb.gameObject);
        yield return null;
    }
}
