using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviourExtended, IPickUpBehaviour
{

    [SerializeField]
    private TransformerBrain brain;

    [SerializeField]
    private List<PowerDeadZone> deadZones;    

    private Vector3 scale;

    public Vector3 Scale => scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out PowerOrbController powerOrb) && powerOrb.CanCollideWith(gameObject))
        {
            if (!Enabled)
            {
                Destroy(powerOrb.gameObject);
                return;
            }
            AcceptEnergy(powerOrb);
        }
    }

    private void AcceptEnergy(PowerOrbController powerOrb)
    {
        StartCoroutine(brain.TransformerCoroutine(this, powerOrb));
    }

    public bool CanBePickedUp()
    {
        return true;
    }

    public void OnPickUp()
    {
        enabled = false;
        foreach (var deadZone in deadZones)
        {
            deadZone.enabled = false;
        }
    }

    public void OnDrop()
    {
        transform.position = Vector3Int.RoundToInt(transform.position);
        enabled = true;
        foreach (var deadZone in deadZones)
        {
            deadZone.enabled = true;
        }
    }
}
