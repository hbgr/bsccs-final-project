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
    private Image image;

    public Image Image
    {
        get => image;
        private set => image = value;
    }

    public abstract void Apply();
}
