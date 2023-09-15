using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviourExtended, IPickUpBehaviour
{

    [SerializeField]
    private TransformerBrain brain;

    private Vector3 scale;

    public Vector3 Scale => scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.TryGetComponent(out PowerOrbController powerOrb) && powerOrb.CanCollideWith(gameObject))
        {
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
    }

    public void OnDrop()
    {
        enabled = true;
    }
}
