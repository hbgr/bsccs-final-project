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
        Instantiate(powerOrb, transformer.transform.position, Quaternion.identity).SetProperties(transformer.transform.rotation * direction, transformer.gameObject);
        Destroy(powerOrb.gameObject);
        yield return null;
    }
}
