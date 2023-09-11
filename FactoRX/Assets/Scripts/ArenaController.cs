using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private ScriptableArenaProperties arenaProps;

    private bool Enabled => GameStateManager.IsState(GameState.Game);

    // Start is called before the first frame update
    void Start()
    {
        arenaProps.currentRadius = arenaProps.startRadius;
        SetRadius(arenaProps.startRadius);
        //StartCoroutine(ShrinkRateIncreaseCoroutine(arenaProps.shrinkRateIncreaseInterval));
        events.MachineCreatedEvent += OnMachineCreated;
        //events.IncreaseArenaRadiusEvent += OnIncreaseArenaRadius;
    }

    private void OnMachineCreated(object sender, Machine machine)
    {
        machine.SetArenaProps(arenaProps);
    }

    // private void OnIncreaseArenaRadius(object send, float amount)
    // {
    //     StartCoroutine(IncreaseRadiusCoroutine(amount));
    // }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        //SetRadius(arenaProps.currentRadius - arenaProps.shrinkRate * Time.deltaTime);
    }

    private void SetRadius(float radius)
    {
        arenaProps.currentRadius = Mathf.Min(radius, arenaProps.maxRadius);
        transform.localScale = new Vector3(2 * radius, 2 * radius, 1);
    }

    // public void IncreaseRadius(float amount)
    // {
    //     StartCoroutine(IncreaseRadiusCoroutine(amount));
    // }

    public float GetCurrentRadius()
    {
        return arenaProps.currentRadius;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Lost game!");
        }
    }

    // private IEnumerator ShrinkRateIncreaseCoroutine(float delay)
    // {
    //     float t = 0;
    //     while (t <= delay)
    //     {
    //         if (Enabled)
    //         {
    //             t += Time.fixedDeltaTime;
    //         }
    //         yield return new WaitForFixedUpdate();
    //     }
    //     arenaProps.shrinkRate *= 1.33f;
    //     StartCoroutine(ShrinkRateIncreaseCoroutine(delay));
    //     yield return null;
    // }

    // private IEnumerator IncreaseRadiusCoroutine(float amount)
    // {
    //     float t = 0;
    //     while (t <= 0.25f)
    //     {
    //         if (Enabled)
    //         {
    //             SetRadius(arenaProps.currentRadius + Time.deltaTime / 0.25f * amount);
    //             t += Time.deltaTime;
    //         }
    //         yield return new WaitForFixedUpdate();
    //     }
    //     yield return null;
    // }
}
