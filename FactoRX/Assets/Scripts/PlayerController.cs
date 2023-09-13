using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourExtended
{
    [SerializeField]
    private ScriptableInputEvents inputEvents;

    [SerializeField]
    private TransformVariable playerTransform;

    [SerializeField]
    private Vector2 _moveInputDir;

    [SerializeField]
    private float _moveSpeed;

    private PickUp heldObject;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        heldObject = null;
        playerTransform.value = transform;
        inputEvents.OnMoveEvent += OnMove;
        inputEvents.OnAction1Event += OnAction1;
        inputEvents.OnAction2Event += OnAction2;
        inputEvents.OnAction3Event += OnAction3;
    }

    // Update is called once per frame
    private void Update()
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

    protected override void OnGameStateChanged(object sender, GameState state)
    {
        // if (activeGameStates.Contains(state))
        // {
        //     inputEvents.OnMoveEvent += OnMove;
        //     inputEvents.OnAction1Event += OnAction1;
        //     inputEvents.OnAction2Event += OnAction2;
        //     inputEvents.OnAction3Event += OnAction3;
        // }
        // else
        // {
        //     inputEvents.OnMoveEvent -= OnMove;
        //     inputEvents.OnAction1Event -= OnAction1;
        //     inputEvents.OnAction2Event -= OnAction2;
        //     inputEvents.OnAction3Event -= OnAction3;
        // }
    }

    private void OnMove(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled)
        {
            _moveInputDir = Vector2.zero;
            return;
        }

        _moveInputDir = context.ReadValue<Vector2>();
    }

    private void OnAction1(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
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

    private void OnAction2(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

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

    private void OnAction3(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
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
    }
}
