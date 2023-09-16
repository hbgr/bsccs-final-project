using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSplitterMachine : MonoBehaviour, IMachine
{
    public Transform Transform => transform;

    public void Activate()
    {
        // Instantiate(powerOrbPrefab, transform.position, Quaternion.identity).SetProperties(orbSpeed, Vector2.left, gameObject);
        // Instantiate(powerOrbPrefab, transform.position, Quaternion.identity).SetProperties(orbSpeed, Vector2.right, gameObject);
    }

    public void SetArenaController(ArenaController arenaController)
    {

    }

    public bool CanBePickedUp()
    {
        return !isActive;
    }

    [SerializeField]
    private PowerOrbController powerOrbPrefab;

    [SerializeField]
    private float orbSpeed;

    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<PowerOrbController>(out PowerOrbController powerOrb))
        {
            if (powerOrb.CanCollideWith(gameObject))
            {
                Activate();
                Destroy(collider.gameObject);
            }
        }
    }
}
