using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractsPlayer : MonoBehaviourExtended
{
    [SerializeField]
    private float power;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.TryGetComponent(out PlayerController player))
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            player.transform.position -= power * Time.fixedDeltaTime * direction;
        }
    }
}
