using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private ScriptableArenaProperties arenaProps;

    [SerializeField]
    private ScriptableAudio arenaMusic;

    private bool Enabled => GameStateManager.IsState(GameState.Game);

    // Start is called before the first frame update
    void Start()
    {
        arenaProps.currentRadius = arenaProps.startRadius;
        SetRadius(arenaProps.startRadius);
        arenaMusic.Play(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
    }

    private void SetRadius(float radius)
    {
        arenaProps.currentRadius = Mathf.Min(radius, arenaProps.maxRadius);
        transform.localScale = new Vector3(2 * radius, 2 * radius, 1);
    }

    public float GetCurrentRadius()
    {
        return arenaProps.currentRadius;
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (!Enabled) return;

    //     if (other.gameObject.GetComponent<PlayerController>())
    //     {

    //     }
    // }
}
