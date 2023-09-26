using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackPlayer : MonoBehaviour
{
    [SerializeField]
    private bool destroyOnKnockback;

    [SerializeField]
    private float knockbackPower;

    public float KnockbackPower => knockbackPower;

    public void DidKnockback()
    {
        if (destroyOnKnockback)
        {
            Destroy(gameObject);
        }
    }
}
