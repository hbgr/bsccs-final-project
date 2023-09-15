using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesPlayer : MonoBehaviour
{
    [SerializeField]
    private bool destroyOnDamage;

    public void DidDamage()
    {
        if (destroyOnDamage)
        {
            Destroy(gameObject);
        }
    }

}
