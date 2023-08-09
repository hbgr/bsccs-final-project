using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 _moveInputDir;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private ArenaController arenaController;

    [SerializeField]
    private BasicMachineController machinePrefab;

    [SerializeField]
    private TextMeshProUGUI gemCounterText;

    [SerializeField]
    private PowerGenerator generator;

    private int _collectedGems;

    private int CollectedGems
    {
        get => _collectedGems;
        set
        {
            _collectedGems = value;
            UpdateGemCounterText(_collectedGems);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CollectedGems = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)(_moveSpeed * Time.deltaTime * _moveInputDir);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<GemController>())
        {
            // Collect gem
            CollectedGems++;
            Destroy(collider.gameObject);
        }
    }

    private void UpdateGemCounterText(int gemCount)
    {
        gemCounterText.text = $"{gemCount}/3";
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInputDir = context.ReadValue<Vector2>();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Create machine
            if (CollectedGems >= 3)
            {
                BasicMachineController machine = Instantiate(machinePrefab, transform.position, Quaternion.identity);
                machine.SetArenaController(arenaController);
                generator.AddMachine(machine);
                CollectedGems -= 3;
            }
        }
    }
}
