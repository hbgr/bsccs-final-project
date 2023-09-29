using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable/Gem")]
public class GemCollectable : ScriptableCollectable
{
    [SerializeField]
    private int value;

    [SerializeField]
    private ScriptableGameEvents events;

    public override void Collect(Collectable collectable)
    {
        events.OnExperienceGained(this, value);
        events.OnScorePoints(this, value * 10);
        collectAudio.Play(collectable.gameObject);
        Destroy(collectable.gameObject);
    }
}
