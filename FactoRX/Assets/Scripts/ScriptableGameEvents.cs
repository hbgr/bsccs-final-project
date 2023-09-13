using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameEvents")]
public class ScriptableGameEvents : ScriptableObject
{
    public event EventHandler<GameState> GameStateChangedEvent;

    public void OnGameStateChanged(object sender, GameState state)
    {
        GameStateChangedEvent?.Invoke(sender, state);
    }

    public event EventHandler<Machine> MachineCreatedEvent;

    public void OnMachineCreated(object sender, Machine machine)
    {
        MachineCreatedEvent?.Invoke(sender, machine);
    }

    public event EventHandler<float> IncreaseArenaRadiusEvent;

    public void OnIncreaseArenaRadius(object sender, float amount)
    {
        IncreaseArenaRadiusEvent?.Invoke(sender, amount);
    }

    public event EventHandler<int> GemCollectedEvent;

    public void OnGemCollected(object sender, int value)
    {
        GemCollectedEvent?.Invoke(sender, value);
    }

    public event EventHandler LevelUpEvent;

    public void OnLevelUp(object sender, EventArgs args)
    {
        LevelUpEvent?.Invoke(sender, args);
    }

    public event EventHandler LevelUpCompletedEvent;

    public void OnLevelUpCompleted(object sender, EventArgs args)
    {
        LevelUpCompletedEvent?.Invoke(sender, args);
    }

    public event EventHandler<int> LoseLifeEvent;

    public void OnLoseLife(object sender, int remainingLives)
    {
        LoseLifeEvent?.Invoke(sender, remainingLives);
    }
}
