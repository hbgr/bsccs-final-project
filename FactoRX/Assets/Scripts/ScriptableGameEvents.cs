using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameEvents")]
public class ScriptableGameEvents : ScriptableObject
{
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
}
