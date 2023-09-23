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

    // public event EventHandler<Machine> MachineCreatedEvent;

    // public void OnMachineCreated(object sender, Machine machine)
    // {
    //     MachineCreatedEvent?.Invoke(sender, machine);
    // }    

    public event EventHandler<int> ExperienceGainedEvent;

    public void OnExperienceGained(object sender, int experienceGained)
    {
        ExperienceGainedEvent?.Invoke(sender, experienceGained);
    }

    public event EventHandler<float> GainPercentLevelEvent;

    public void OnGainPercentLevelEvent(object sender, float factor)
    {
        GainPercentLevelEvent?.Invoke(sender, factor);
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

    public event EventHandler<ShieldCollectable> ShieldCollectedEvent;

    public void OnShieldCollected(object sender, ShieldCollectable shield)
    {
        ShieldCollectedEvent?.Invoke(sender, shield);
    }

    public event EventHandler<int> ScorePointsEvent;

    public void OnScorePoints(object sender, int points)
    {
        ScorePointsEvent?.Invoke(sender, points);
    }

    public event EventHandler<Abyss> AbyssDestroyedEvent;

    public void OnAbyssDestroyed(object sender, Abyss abyss)
    {
        AbyssDestroyedEvent?.Invoke(sender, abyss);
    }
}
