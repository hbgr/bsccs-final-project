using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    public bool CanMove => !knockbackActive;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private PickUpIndicator pickUpIndicator;

    [SerializeField]
    private RotationIndicator rotationIndicator;

    [SerializeField]
    private float pickUpRange;

    [SerializeField]
    private GameObject shieldObject;

    [SerializeField]
    private TextMeshPro shieldText;

    [SerializeField]
    private ScriptableAudio hurtAudio;

    [SerializeField]
    private ScriptableAudio knockbackAudio;

    private PickUp heldObject;

    private bool Shielded => shieldDuration > 0f;

    private float shieldDuration;

    private Coroutine knockbackCoroutine;

    private bool knockbackActive = false;

    protected override void Awake()
    {
        base.Awake();

        playerTransform.value = transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        heldObject = null;
        shieldDuration = 0f;

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

        // Handle shield visibility
        if (shieldDuration > 0f)
        {
            shieldDuration -= Time.deltaTime;
            if (!shieldObject.activeSelf)
            {
                shieldObject.SetActive(true);
            }
            shieldText.text = $"{shieldDuration:F1}";
        }
        else
        {
            shieldDuration = 0f;
            if (shieldObject.activeSelf)
            {
                shieldObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!Enabled) return;

        if (CanMove)
        {
            transform.position += (Vector3)(moveSpeed * Time.deltaTime * moveInputDir);
        }

        if (moveInputDir != Vector2.zero && CanMove)
        {
            facingDirection = moveInputDir;

            float rotationAmount = Mathf.PingPong(Time.time * 75f, 14f) - 7f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAmount));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        // Handle pick up / put down indicators and held object positioning
        if (heldObject != null)
        {
            heldObject.transform.position = Vector3Int.RoundToInt(transform.position + transform.rotation * facingDirection);
            pickUpIndicator.gameObject.SetActive(false);

            if (heldObject.TryGetComponent(out Rotatable rotatable))
            {
                rotationIndicator.SetPosition(rotatable);
                rotationIndicator.gameObject.SetActive(true);
                rotationIndicator.enabled = true;
            }
            else
            {
                rotationIndicator.enabled = false;
                rotationIndicator.gameObject.SetActive(false);
            }
        }
        else
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

            rotationIndicator.enabled = false;
            rotationIndicator.gameObject.SetActive(false);
        }

        // Handle fliping player sprite to match direction
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

    private void OnTriggerStay2D(Collider2D collider)
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

        if (collider.gameObject.TryGetComponent(out KnockbackPlayer knockbacker))
        {
            TakeKnockback(knockbacker);
            knockbacker.DidKnockback();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Enabled) return;

        if (other.gameObject.GetComponent<ArenaController>())
        {
            lives.Lives--;
            events.OnLoseLife(this, lives.Lives);
            hurtAudio.Play(gameObject);
        }
    }

    private void TakeDamage()
    {
        if (!Shielded)
        {
            lives.Lives--;
            events.OnLoseLife(this, lives.Lives);
            hurtAudio.Play(gameObject);
        }
        else
        {
            shieldDuration -= 0.5f;
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
        if (shieldDuration <= duration * 1.5f)
        {
            shieldDuration += duration;
        }
        else if (shieldDuration <= duration * 2.5f)
        {
            shieldDuration = duration * 2.5f;
        }
    }

    private void Respawn()
    {
        AddInvincibility(3f);
        transform.position = spawnPosition.position;
    }

    private void TakeKnockback(KnockbackPlayer source)
    {
        if (!Shielded)
        {
            knockbackAudio.Play(gameObject);
            var dir = (transform.position - source.transform.position).normalized;
            var power = source.KnockbackPower;
            if (knockbackCoroutine != null)
            {
                StopCoroutine(knockbackCoroutine);
            }
            knockbackCoroutine = StartCoroutine(KnockbackCoroutine(dir, power));
        }
        else
        {
            shieldDuration -= 0.5f;
        }
    }

    private IEnumerator KnockbackCoroutine(Vector3 direction, float power)
    {
        knockbackActive = true;

        float t = 0;
        while (t <= 0.25f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.position += power * Time.fixedDeltaTime * direction;
                power *= 0.8f;
            }
            yield return new WaitForFixedUpdate();
        }

        knockbackActive = false;
        yield return null;
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
                var blockers = Physics2D.OverlapCircleAll(heldObject.transform.position, 0.1f).
                    Where(c => c.gameObject.TryGetComponent(out PlacementBlocker p) && p.gameObject != heldObject.gameObject).
                    ToList();

                if (blockers.Count <= 0)
                {
                    heldObject.OnDrop();
                    heldObject = null;
                }
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
