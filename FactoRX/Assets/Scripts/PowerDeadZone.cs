using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PowerDeadZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out PowerOrbController powerOrb) && enabled)
        {
            Destroy(powerOrb.gameObject);
        }
    }
}
