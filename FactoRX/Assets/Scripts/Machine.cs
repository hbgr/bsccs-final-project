using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Machine : MonoBehaviour, IPickUpBehaviour
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private ScriptableGameState gameState;

    [SerializeField]
    private ScriptableMachineBrain brain;

    public bool isActive = false;

    private bool Enabled => GameStateManager.IsState(GameState.Game);

    [SerializeField]
    private ScriptableArenaProperties arenaProps;

    // Start is called before the first frame update
    void Start()
    {
        events.OnMachineCreated(this, this);
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
            if (!isActive)
            {
                Destroy(collider.gameObject);
                Activate();
            }
            else
            {
                powerOrb.transform.position = transform.position;
                powerOrb.SetProperties(transform.rotation * Vector3.up);
            }
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
