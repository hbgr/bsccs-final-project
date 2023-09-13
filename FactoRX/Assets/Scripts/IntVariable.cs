using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
    private int val;

    public int Value
    {
        get => val;
        set
        {
            val = value;
            OnValueChanged(this, this);
        }
    }

    [SerializeField]
    private int resetValue;

    [SerializeField]
    private bool resetOnEnable;

    private void OnEnable()
    {
        if (resetOnEnable)
        {
            Value = resetValue;
        }
    }

    public event EventHandler<IntVariable> ValueChangedEvent;

    private void OnValueChanged(object sender, IntVariable intVar)
    {
        ValueChangedEvent?.Invoke(sender, intVar);
    }
}
