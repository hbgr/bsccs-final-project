using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelUpBehaviour : ScriptableObject
{
    [SerializeField]
    private string behaviourName;
    public string Name
    {
        get => behaviourName;
        private set => behaviourName = value;
    }

    [SerializeField]
    private string behaviourDescription;

    public string Description
    {
        get => behaviourDescription;
        private set => behaviourDescription = value;
    }

    [SerializeField]
    private Sprite sprite;

    public Sprite Sprite
    {
        get => sprite;
        private set => sprite = value;
    }

    [SerializeField]
    [Range(0, 200)]
    private int weight;

    public int Weight => weight;

    public abstract void Apply();
}
