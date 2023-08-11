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

    private int _requiredGems;

    private int RequiredGems
    {
        get => _requiredGems;
        set
        {
            _requiredGems = value;
            UpdateGemCounterText(_collectedGems, _requiredGems);
        }
    }

    private int _collectedGems;

    private int CollectedGems
    {
        get => _collectedGems;
        set
        {
            _collectedGems = value;
            UpdateGemCounterText(_collectedGems, _requiredGems);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CollectedGems = 0;
        RequiredGems = 3;
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
            CollectedGems++;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.GetComponent<IBullet>() != null)
        {
            Debug.Log("You died!");
        }
    }

    private void UpdateGemCounterText(int gemCount, int requiredGems)
    {
        gemCounterText.text = $"{gemCount}/{requiredGems}";
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
            if (CollectedGems >= RequiredGems)
            {
                BasicMachineController machine = Instantiate(machinePrefab, transform.position, Quaternion.identity);
                CollectedGems -= RequiredGems;
                RequiredGems += 3;
            }
        }
    }
}
