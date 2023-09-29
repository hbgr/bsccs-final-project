using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Systems/PowerOrbManager")]
public class PowerOrbManager : ScriptableObject
{
    [SerializeField]
    private float generatorCooldown;

    private float currentGeneratorCooldown;

    public float GeneratorCooldown
    {
        get => currentGeneratorCooldown;
        set => currentGeneratorCooldown = value;
    }

    [SerializeField]
    private int maxSplits;

    private int currentMaxSplits;

    public int MaxSplits
    {
        get => currentMaxSplits;
        set => currentMaxSplits = value;
    }

    [SerializeField]
    private float orbLifetime;

    private float currentOrbLifetime;

    public float OrbLifetime
    {
        get => currentOrbLifetime;
        set => currentOrbLifetime = value;
    }

    [SerializeField]
    private float orbSpeed;

    private float currentOrbSpeed;

    public float OrbSpeed
    {
        get => currentOrbSpeed;
        set => currentOrbSpeed = value;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        currentMaxSplits = maxSplits;
        currentOrbLifetime = orbLifetime;
        currentOrbSpeed = orbSpeed;
        currentGeneratorCooldown = generatorCooldown;
    }
}
