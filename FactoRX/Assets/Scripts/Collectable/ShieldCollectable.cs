using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable/Shield")]
public class ShieldCollectable : ScriptableCollectable
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private float duration;

    public override void Collect(Collectable collectable)
    {
        events.OnShieldCollected(this, duration);
        collectAudio.Play(collectable.gameObject);
        Destroy(collectable.gameObject);
    }
}
