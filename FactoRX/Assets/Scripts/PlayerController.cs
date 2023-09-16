using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviourExtended
{
    [SerializeField]
    private ScriptableInputEvents inputEvents;

    [SerializeField]
    private TransformVariable playerTransform;

    [SerializeField]
    private LivesManager lives;

    [SerializeField]
    private Transform spawnPosition;

    private Vector2 moveInputDir;

    private Vector2 facingDirection;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private PickUpIndicator pickUpIndicator;

    [SerializeField]
    private float pickUpRange;

    [SerializeField]
    private GameObject shielded;

    private PickUp heldObject;

    private bool Damageable => invincibilityDuration <= 0f;

    private float invincibilityDuration;

    protected override void Awake()
    {
        base.Awake();

        playerTransform.value = transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        heldObject = null;
        invincibilityDuration = 0f;

        inputEvents.OnMoveEvent += OnMove;
        inputEvents.OnAction1Event += OnAction1;
        inputEvents.OnAction2Event += OnAction2;
        inputEvents.OnAction3Event += OnAction3;

        events.LoseLifeEvent += OnLoseLife;
        events.ShieldCollectedEvent += OnShieldCollected;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Enabled) return;

        if (invincibilityDuration > 0f)
        {
            invincibilityDuration -= Time.deltaTime;
            if (!shielded.activeSelf)
            {
                shielded.SetActive(true);
            }
        }
        else
        {
            if (shielded.activeSelf)
            {
                shielded.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!Enabled) return;

        transform.position += (Vector3)(moveSpeed * Time.deltaTime * moveInputDir);
        if (moveInputDir != Vector2.zero)
        {
            facingDirection = moveInputDir;
        }

        if (heldObject != null)
        {
            heldObject.transform.position = Vector3Int.RoundToInt(transform.position + transform.rotation * facingDirection);
        }

        // Handle fliping sprite to match direction
        if (moveInputDir.x > 0)
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.flipX = false;
        }
        else if (moveInputDir.x < 0)
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.flipX = true;
        }

        // Handle rotation when moving
        if (moveInputDir != Vector2.zero)
        {
            float rotationAmount = Mathf.PingPong(Time.time * 75f, 14f) - 7f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAmount));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        if (heldObject == null)
        {
            var pickups = Physics2D.OverlapCircleAll(transform.position, pickUpRange).
                    Where(c => c.gameObject.TryGetComponent(out PickUp p) && p.CanBePickedUp()).
                    OrderBy(c => Vector2.Distance(c.transform.position, transform.position)).
                    Select(c => c.gameObject.GetComponent<PickUp>()).
                    ToList();

            if (pickups.Count > 0)
            {
                pickUpIndicator.gameObject.SetActive(true);
                pickUpIndicator.enabled = true;
                pickUpIndicator.SetPosition(pickups[0]);
            }
            else
            {
                pickUpIndicator.enabled = false;
                pickUpIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            pickUpIndicator.gameObject.SetActive(false);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        inputEvents.OnMoveEvent -= OnMove;
        inputEvents.OnAction1Event -= OnAction1;
        inputEvents.OnAction2Event -= OnAction2;
        inputEvents.OnAction3Event -= OnAction3;

        events.LoseLifeEvent -= OnLoseLife;
        events.ShieldCollectedEvent -= OnShieldCollected;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Enabled) return;

        if (collider.gameObject.TryGetComponent(out Collectable collectable))
        {
            collectable.Collect();
        }

        if (collider.gameObject.TryGetComponent(out DamagesPlayer damager))
        {
            TakeDamage();
            damager.DidDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Enabled) return;

        if (other.gameObject.GetComponent<ArenaController>())
        {
            lives.Lives--;
            events.OnLoseLife(this, lives.Lives);
        }
    }

    private void TakeDamage()
    {
        if (Damageable)
        {
            lives.Lives--;
            events.OnLoseLife(this, lives.Lives);
        }
    }

    private void OnLoseLife(object sender, int remainingLives)
    {
        // drop held object
        if (heldObject != null)
        {
            heldObject.OnDrop();
            heldObject = null;
        }

        if (remainingLives > 0)
        {
            Respawn();
        }
    }

    private void OnShieldCollected(object sender, ShieldCollectable shield)
    {
        AddInvincibility(shield.Duration);
    }

    private void AddInvincibility(float duration)
    {
        invincibilityDuration += duration;
    }

    private void Respawn()
    {
        AddInvincibility(1f);
        transform.position = spawnPosition.position;
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
            moveInputDir = Vector2.zero;
            return;
        }

        moveInputDir = context.ReadValue<Vector2>();
    }

    private void OnAction1(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            if (heldObject != null)
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
                var pickups = Physics2D.OverlapCircleAll(transform.position, pickUpRange).
                    Where(c => c.gameObject.TryGetComponent(out PickUp p) && p.CanBePickedUp()).
                    OrderBy(c => Vector2.Distance(c.transform.position, transform.position)).
                    Select(c => c.gameObject.GetComponent<PickUp>()).
                    ToList();

                if (pickups.Count > 0)
                {
                    heldObject = pickups[0];
                    heldObject.OnPickUp();
                }
            }
            else
            {
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
            if (heldObject != null)
            {
                if (heldObject.TryGetComponent(out Rotatable rotatable))
                {
                    rotatable.RotateBy(-90);
                }
            }
        }
    }
}
