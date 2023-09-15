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

    private Vector3 scale;

    public Vector3 Scale => scale;

    // Start is called before the first frame update
    void Start()
    {
        events.OnMachineCreated(this, this);
        energy = 0;
        scale = transform.localScale;
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
