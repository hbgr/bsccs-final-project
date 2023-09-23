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
        arenaProps.radius = transform.localScale.x / 2f;
        arenaMusic.Play(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
    }
}
