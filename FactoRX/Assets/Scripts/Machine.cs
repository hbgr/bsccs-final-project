using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Machine : MonoBehaviourExtended, IPickUpBehaviour
{
    [SerializeField]
    private MachineBrain brain;

    public bool isActive = false;

    [SerializeField]
    private ScriptableArenaProperties arenaProps;

    [SerializeField]
    private int energy;

    // Start is called before the first frame update
    void Start()
    {
        events.OnMachineCreated(this, this);
        energy = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Activate()
    {
        StartCoroutine(brain.MachineCoroutine(this, arenaProps));
    }

    public void SetArenaProps(ScriptableArenaProperties arenaProps)
    {
        this.arenaProps = arenaProps;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.TryGetComponent(out PowerOrbController powerOrb) && powerOrb.CanCollideWith(gameObject))
        {
            // if (!isActive)
            // {
            //     Destroy(collider.gameObject);
            //     Activate();
            // }
            // else
            // {
            //     powerOrb.transform.position = transform.position;
            //     powerOrb.SetProperties(transform.rotation * Vector3.up);
            // }

            Destroy(collider.gameObject);
            GainEnergy(1);
        }
    }

    public void GainEnergy(int e)
    {
        energy += e;
        if (energy >= brain.ActivationEnergy)
        {
            Activate();
            energy = 0;
        }
    }

    public bool CanBePickedUp()
    {
        return !isActive;
    }

    public void OnPickUp()
    {

    }

    public void OnDrop()
    {

    }
}
