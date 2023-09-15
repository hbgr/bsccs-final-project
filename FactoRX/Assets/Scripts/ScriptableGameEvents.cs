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

    public event EventHandler<int> ExperienceGainedEvent;

    public void OnExperienceGained(object sender, int experienceGained)
    {
        ExperienceGainedEvent?.Invoke(sender, experienceGained);
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

    public event EventHandler<ScriptableAudio> PlayAudioEvent;

    public void OnPlayAudio(GameObject obj, ScriptableAudio audio)
    {
        PlayAudioEvent?.Invoke(obj, audio);
    }

    public event EventHandler<ShieldCollectable> ShieldCollectedEvent;

    public void OnShieldCollected(object sender, ShieldCollectable shield)
    {
        ShieldCollectedEvent?.Invoke(sender, shield);
    }    
}
