using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ScriptableInputEvents inputEvents;

    [SerializeField]
    private ScriptableGameState gameState;

    [SerializeField]
    private Vector2 _moveInputDir;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private Machine machinePrefab;

    [SerializeField]
    private Machine splitterMachinePrefab;

    [SerializeField]
    private Machine gemMachinePrefab;

    private PickUp heldObject;    

    private bool Enabled => GameStateManager.IsState(GameState.Game);

    // Start is called before the first frame update
    void Start()
    {
        heldObject = null;
        inputEvents.OnMoveEvent += OnMove;
        inputEvents.OnAction1Event += OnAction1;
        inputEvents.OnAction2Event += OnAction2;
        inputEvents.OnAction3Event += OnAction3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!Enabled) return;

        transform.position += (Vector3)(_moveSpeed * Time.deltaTime * _moveInputDir);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.gameObject.TryGetComponent(out Collectable collectable))
        {
            collectable.Collect();
        }

        if (collider.gameObject.GetComponent<DamagesPlayer>())
        {
            Debug.Log("You died!");
        }
    }    

    // private Machine TryBuildMachine(Machine machine, int cost)
    // {
    //     if (CollectedGems >= cost)
    //     {
    //         Machine m = Instantiate(machine, transform.position, Quaternion.identity);
    //         CollectedGems -= cost;
    //         return m;
    //     }
    //     return null;
    // }

    // public void OnMove(InputAction.CallbackContext context)
    // {
    //     if (!Active) return;
    //     _moveInputDir = context.ReadValue<Vector2>();
    // }

    public void OnMove(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        _moveInputDir = context.ReadValue<Vector2>();
    }

    // public void OnAction(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         if (heldObject == null)
    //         {
    //             var pickups = Physics2D.OverlapCircleAll(transform.position, 1f).
    //                 Where(c => c.gameObject.TryGetComponent(out PickUp p) && p.CanBePickedUp()).
    //                 OrderBy(c => Vector2.Distance(c.transform.position, transform.position)).
    //                 Select(c => c.gameObject.GetComponent<PickUp>()).
    //                 ToList();

    //             if (pickups.Count > 0)
    //             {
    //                 heldObject = pickups[0];
    //                 heldObject.gameObject.SetActive(false);
    //             }
    //         }
    //         else
    //         {
    //             //drop held object
    //             heldObject.transform.position = transform.position;
    //             heldObject.gameObject.SetActive(true);
    //             heldObject = null;
    //         }
    //     }
    // }

    // public void OnAction1(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         // Create basic machine
    //         TryBuildMachine(machinePrefab, 3);
    //     }
    // }

    public void OnAction1(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        // if (context.performed)
        // {
        //     // Create basic machine
        //     TryBuildMachine(machinePrefab, 3);
        // }

        if (heldObject == null)
        {
            var rotatables = Physics2D.OverlapCircleAll(transform.position, 1f).
                Where(c => c.gameObject.TryGetComponent(out Rotatable r)).
                OrderBy(c => Vector2.Distance(c.transform.position, transform.position)).
                Select(c => c.gameObject.GetComponent<Rotatable>()).
                ToList();

            if (rotatables.Count > 0)
            {
                rotatables[0].RotateBy(-90);
            }
        }
        else
        {
            if (heldObject.TryGetComponent(out Rotatable rotatable))
            {
                rotatable.RotateBy(-90);
            }
        }

    }

    // public void OnAction2(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         // Create basic machine
    //         TryBuildMachine(gemMachinePrefab, 5);
    //     }
    // }

    public void OnAction2(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        // if (context.performed)
        // {
        //     // Create basic machine
        //     TryBuildMachine(gemMachinePrefab, 5);
        // }

        if (context.performed)
        {
            if (heldObject == null)
            {
                var pickups = Physics2D.OverlapCircleAll(transform.position, 1f).
                    Where(c => c.gameObject.TryGetComponent(out PickUp p) && p.CanBePickedUp()).
                    OrderBy(c => Vector2.Distance(c.transform.position, transform.position)).
                    Select(c => c.gameObject.GetComponent<PickUp>()).
                    ToList();

                if (pickups.Count > 0)
                {
                    heldObject = pickups[0];
                    heldObject.gameObject.SetActive(false);
                }
            }
            else
            {
                //drop held object
                heldObject.transform.position = transform.position;
                heldObject.gameObject.SetActive(true);
                heldObject.OnDrop();
                heldObject = null;
            }
        }
    }

    // public void OnAction3(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         // Create basic machine
    //         TryBuildMachine(splitterMachinePrefab, 4);
    //     }
    // }

    public void OnAction3(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        // if (context.performed)
        // {
        //     // Create basic machine
        //     TryBuildMachine(splitterMachinePrefab, 4);
        // }

        if (heldObject == null)
        {
            var rotatables = Physics2D.OverlapCircleAll(transform.position, 1f).
                Where(c => c.gameObject.TryGetComponent(out Rotatable r)).
                OrderBy(c => Vector2.Distance(c.transform.position, transform.position)).
                Select(c => c.gameObject.GetComponent<Rotatable>()).
                ToList();

            if (rotatables.Count > 0)
            {
                rotatables[0].RotateBy(90);
            }
        }
        else
        {
            if (heldObject.TryGetComponent(out Rotatable rotatable))
            {
                rotatable.RotateBy(90);
            }
        }
    }
}
