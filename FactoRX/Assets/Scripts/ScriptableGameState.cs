using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Game,
    Menu
}

[CreateAssetMenu(menuName = "GameState")]
public class ScriptableGameState : ScriptableObject
{
    public GameState state;
}
